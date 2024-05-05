using eArticles.API.Contracts.Article;
using eArticles.API.Extensions;
using eArticles.API.Models;
using eArticles.API.Persistance;
using eArticles.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Security.Claims;
using System.Text.Json;
using System.Xml.Linq;
using System.Drawing.Imaging;
using System.IO;

namespace eArticles.API.Controllers;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
public class ArticlesController : ControllerBase
{
    readonly IArticleService _articlesService;
    readonly IUserService _usersService;
    readonly IWebHostEnvironment _environment;

    public ArticlesController(IArticleService articlesService, IUserService userService, IWebHostEnvironment environment)
    {
        _articlesService = articlesService;
        _usersService = userService;
        _environment = environment;
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        var getArticleResult = await _articlesService.GetById(id);
        if (getArticleResult.IsError)
        {
            return NotFound(getArticleResult.FirstError.Description);
        }
        return Ok(getArticleResult.Value.AsDto());
    }

    [HttpGet("my")]
    public async Task<IActionResult> GetUserPage(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] Guid? contentTypeId = null,
        [FromQuery] Guid? categoryId = null,
        [FromQuery] string? order = null,
        [FromQuery] IEnumerable<Guid>? tagIds = null
    )
    {
        if (pageNumber <= 0)
        {
            return NotFound("PageNumber can't be less or equal to 0");
        }
        Guid userId;
        if (!Guid.TryParse(
           User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
           out userId
       ))
        {
            return BadRequest("Wrong user id format");
        };
        var getUserResult = await _usersService.GetUserById(userId);
        if (getUserResult.IsError)
        {
            return BadRequest(getUserResult.FirstError.Description);
        }
        var user = getUserResult.Value;
        var getArticlesPageResult = await _articlesService.GetPage(pageNumber,
                                                                   pageSize,
                                                                   userId: user.Id,
                                                                   contentTypeId: contentTypeId,
                                                                   categoryId: categoryId,
                                                                   order: order,
                                                                   tagIds: tagIds);
        var getTotalItemsResult = await _articlesService.GetTotalItems(user.Id);
        if (getArticlesPageResult.IsError)
        {
            return NotFound(getArticlesPageResult.FirstError);
        }
        var articleDTOs = new List<ArticleResponse>();
        foreach (var article in getArticlesPageResult.Value)
        {
            articleDTOs.Add(article.AsDto());
        }
        return Ok(new ArticlePageResponse(articleDTOs, getTotalItemsResult.Value));
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetPage(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 5,
        [FromQuery] Guid? contentTypeId = null,
        [FromQuery] Guid? categoryId = null,
        [FromQuery] string? order = null,
        [FromQuery] IEnumerable<Guid>? tagIds = null
    )
    {
        if (pageNumber <= 0)
        {
            return NotFound("PageNumber can't be less or equal to 0");
        }
        var getArticlesPageResult = await _articlesService.GetPage(
            currentPage: pageNumber,
            pageSize: pageSize,
            contentTypeId: contentTypeId,
            categoryId: categoryId,
            order: order,
            tagIds: tagIds);

        if (getArticlesPageResult.IsError)
        {
            return NotFound(getArticlesPageResult.FirstError.Description);
        }
        var articles = getArticlesPageResult.Value;
        int totalArticles = articles.Count();
        List<ArticleResponse> articleDTOs = new List<ArticleResponse>();
        foreach (var article in articles)
        {
            articleDTOs.Add(article.AsDto());
        }
        return Ok(new ArticlePageResponse(articleDTOs, totalArticles));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateArticleRequest articleRequest)
    {

        var image = articleRequest.image;
        var articleDto = JsonSerializer.Deserialize<CreateArticleRequestDto>(articleRequest.json);
        if (articleDto is null)
        {
            return BadRequest("json format error");
        }
        if (!TryValidateModel(articleDto))
        {
            return ValidationProblem(ModelState);
        }
        Guid userId;
        if (!Guid.TryParse(
           User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
           out userId
       ))
        {
            return BadRequest("Wrong user id format");
        };
        var getUserResult = await _usersService.GetUserById(userId);
        if (getUserResult.IsError)
        {
            return NotFound(getUserResult.FirstError.Description);
        }
        Article artcile = new Article()
        {
            Title = articleDto.title,
            Description = articleDto.description,
            Content = articleDto.content,
            CategoryId = articleDto.categoryId,
            ContentTypeId = articleDto.contentTypeId
        };
        artcile.User = getUserResult.Value;
        string? path;
        if (articleRequest.image is null)
        {
            path = Path.Combine(_environment.WebRootPath, @"images\Placeholder.png");
        }
        else
        {
            path = Path.Combine(_environment.WebRootPath, "images", articleRequest.image.FileName);
            using (var stream = articleRequest.image.OpenReadStream())
            {
                var newImage = new Bitmap(stream);
                Bitmap resizedImage = ResizeImage(newImage, 1024, 576);
                resizedImage.Save(path);
                stream.Close();
            }
        }
        artcile.ImagePath = path;

        var createArticleResult = await _articlesService.Create(artcile, articleDto.tagIds);
        if (createArticleResult.IsError)
        {
            return BadRequest(createArticleResult.FirstError.Description);
        }
        var createdArticleResponse = createArticleResult.Value.AsDto();
        return CreatedAtAction(
            actionName: nameof(GetById),
            routeValues: new { id = createdArticleResponse.Id },
            value: createdArticleResponse);
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateArticleRequest updateArticleDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Guid userId;
        if (!Guid.TryParse(
           User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
           out userId
       ))
        {
            return BadRequest("Wrong user id format");
        };
        var getUserResult = await _usersService.GetUserById(userId);
        if (getUserResult.IsError)
        {
            return BadRequest(getUserResult.FirstError.Description);
        }
        var getArticleResult = await _articlesService.GetById(id);
        if (getArticleResult.IsError)
        {
            return NotFound(getArticleResult.FirstError.Description);
        }
        var article = getArticleResult.Value;
        var user = getUserResult.Value;
        if (article.UserId != user.Id)
        {
            return Forbid();
        }
        var articleToUpdate = new Article()
        {
            Title = updateArticleDto.Title,
            Description = updateArticleDto.Description,
            Content = updateArticleDto.Content,
            CategoryId = updateArticleDto.CategoryId,
            ContentTypeId = updateArticleDto.ContentTypeId
        };
        articleToUpdate.Id = article.Id;
        articleToUpdate.User = article.User;
        var updatedArticleResult = await _articlesService.Update(articleToUpdate, updateArticleDto.TagIds);
        if (updatedArticleResult.IsError)
        {
            return NotFound(updatedArticleResult.FirstError.Description);
        }
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        Guid userId;
        if (!Guid.TryParse(
           User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
           out userId
       ))
        {
            return BadRequest("Wrong user id format");
        };
        var getUserResult = await _usersService.GetUserById(userId);
        if (getUserResult.IsError)
        {
            return NotFound(getUserResult.FirstError.Description);
        }
        var getArticleResult = await _articlesService.GetById(id);
        if (getArticleResult.IsError)
        {
            return NotFound(getUserResult.FirstError.Description);
        }
        var article = getArticleResult.Value;
        var user = getUserResult.Value;
        if (article.UserId != user.Id)
        {
            return Forbid();
        }
        var deleteArticleResult = await _articlesService.Delete(id);
        if (deleteArticleResult.IsError)
        {
            return NotFound(deleteArticleResult.FirstError.Description);
        }
        return NoContent();
    }

    public static Bitmap ResizeImage(Image image, int width, int height)
    {
        var destImage = new Bitmap(width, height);

        using (var graphics = Graphics.FromImage(destImage))
        {
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            float scaleX = (float)width / image.Width;
            float scaleY = (float)height / image.Height;

            float scale = Math.Max(scaleX, scaleY);

            int newWidth = (int)(image.Width * scale);
            int newHeight = (int)(image.Height * scale);

            int posX = (width - newWidth) / 2;
            int posY = (height - newHeight) / 2;

            graphics.DrawImage(image, posX, posY, newWidth, newHeight);
        }

        return destImage;
    }
}
