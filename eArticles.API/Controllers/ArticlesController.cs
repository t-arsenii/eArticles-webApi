using eArticles.API.Data.Dtos;
using eArticles.API.Extensions;
using eArticles.API.Models;
using eArticles.API.Persistance;
using eArticles.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eArticles.API.Controllers;

[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ArticlesController : ControllerBase
{
    readonly IArticleService _articlesService;
    readonly IUsersRepository _usersRepo;

    public ArticlesController(IArticleService articlesService, IUsersRepository usersRepo)
    {
        _articlesService = articlesService;
        _usersRepo = usersRepo;
    }

    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(Guid id)
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
        var getUserResult = await _usersRepo.GetUserById(userId);
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
        var articleDTOs = new List<ArticleDto>();
        foreach (var article in getArticlesPageResult.Value)
        {
            articleDTOs.Add(article.AsDto());
        }
        return Ok(new PageArticleDto(articleDTOs, getTotalItemsResult.Value));
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
        List<ArticleDto> articleDTOs = new List<ArticleDto>();
        foreach (var article in articles)
        {
            articleDTOs.Add(article.AsDto());
        }
        return Ok(new PageArticleDto(articleDTOs, totalArticles));
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateArticleDto articleDto)
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
        var getUserResult = await _usersRepo.GetUserById(userId);
        if (getUserResult.IsError)
        {
            return NotFound(getUserResult.FirstError.Description);
        }
        Article artcile = new Article()
        {
            Title = articleDto.Title,
            Description = articleDto.Description,
            Content = articleDto.Content,
            CategoryId = articleDto.CategoryId,
            ContentTypeId = articleDto.ContentTypeId,
            Img_Url = articleDto.Img_Url
        };
        artcile.User = getUserResult.Value;
        var createArticleResult = await _articlesService.Create(artcile, articleDto.TagIds);
        if (createArticleResult.IsError)
        {
            return BadRequest(createArticleResult.FirstError.Description);
        }
        return Ok(createArticleResult.Value.AsDto());
    }

    [Authorize]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateArticleDto updateArticleDto)
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
        var getUserResult = await _usersRepo.GetUserById(userId);
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
            ContentTypeId = updateArticleDto.ContentTypeId,
            Img_Url = updateArticleDto.Img_Url
        };
        articleToUpdate.Id = article.Id;
        articleToUpdate.User = article.User;
        var updatedArticleResult = await _articlesService.Update(articleToUpdate, updateArticleDto.TagIds);
        if (updatedArticleResult.IsError)
        {
            return NotFound(updatedArticleResult.FirstError.Description);
        }
        return Ok(updatedArticleResult.Value.AsDto());
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
        var getUserResult = await _usersRepo.GetUserById(userId);
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
        if (getArticleResult.IsError)
        {
            return NotFound(getArticleResult.FirstError.Description);
        }
        return Ok($"Deleted article with id {id}");
    }
}
