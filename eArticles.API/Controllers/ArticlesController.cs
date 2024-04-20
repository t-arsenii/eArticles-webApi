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

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> Get(int id)
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
        [FromQuery] string? contentType = null,
        [FromQuery] string? category = null,
        [FromQuery] string? order = null,
        [FromQuery] string[]? tags = null
    )
    {
        if (pageNumber <= 0)
        {
            return NotFound("PageNumber can't be less or equal to 0");
        }
        var userId = int.Parse(
            User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!
        );
        var getUserResult = await _usersRepo.GetUserById(userId);
        if (getUserResult.IsError)
        {
            return BadRequest(getUserResult.FirstError.Description);
        }
        var user = getUserResult.Value;
        var getArticlesPageResult = await _articlesService.GetPage(pageNumber,
                                                                   pageSize,
                                                                   userId: user.Id,
                                                                   contentType: contentType,
                                                                   category: category,
                                                                   order: order,
                                                                   tags: tags);
        var getTotalItemsResult = await _articlesService.GetTotalItems(user.Id);
        if (getArticlesPageResult.IsError)
        {
            return NotFound(getArticlesPageResult.FirstError);
        }
        List<ArticleDto> articleDTOs = new List<ArticleDto>();
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
        [FromQuery] string? contentType = null,
        [FromQuery] string? category = null,
        [FromQuery] string? order = null,
        [FromQuery] string[]? tags = null
    )
    {
        if (pageNumber <= 0)
        {
            return NotFound("PageNumber can't be less or equal to 0");
        }
        var getArticlesPageResult = await _articlesService.GetPage(
            currentPage: pageNumber,
            pageSize: pageSize,
            contentType: contentType,
            category: category,
            order: order,
            tags: tags);

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
        var userId = int.Parse(
            User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!
        );
        var getUserResult = await _usersRepo.GetUserById(userId);
        if (getUserResult.IsError)
        {
            return NotFound(getUserResult.FirstError.Description);
        }
        Article artcile = articleDto.AsArticle();
        artcile.User = getUserResult.Value;
        var createArticleResult = await _articlesService.Create(artcile, articleDto.TagIds);
        if (createArticleResult.IsError)
        {
            return BadRequest(createArticleResult.FirstError.Description);
        }
        return Ok(createArticleResult.Value.AsDto());
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateArticleDto updateArticleDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userId = int.Parse(
            User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!
        );
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
        var articleToUpdate = updateArticleDto.AsArticle();
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
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = int.Parse(
            User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!
        );
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
