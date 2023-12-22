using eArticles.API.Data.Dtos;
using eArticles.API.Extensions;
using eArticles.API.Models;
using eArticles.API.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eArticles.API.Controllers;

[Route("api/[controller]")]
public class ArticlesController : ControllerBase
{
    readonly IArticlesRepository _articleRepo;
    readonly IUsersRepository _usersRepo;

    public ArticlesController(IArticlesRepository repository, IUsersRepository usersRepo)
    {
        _articleRepo = repository;
        _usersRepo = usersRepo;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        Article? article = await _articleRepo.Get(id);
        if (article == null)
        {
            return NotFound();
        }
        return Ok(article.AsDto());
    }

    [Authorize]
    [HttpGet("my")]
    public async Task<IActionResult> GetUserPage(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 5
    )
    {
        if (pageNumber <= 0)
        {
            return NotFound();
        }
        var userId = int.Parse(
            User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!
        );
        var user = await _usersRepo.GetUserById(userId);
        if (user == null)
        {
            return BadRequest();
        }

        IEnumerable<Article>? articles = await _articleRepo.GetPage(pageNumber, pageSize, user.Id);
        int totalArticles = await _articleRepo.GetTotalItems(user.Id);
        if (articles == null || !articles.Any())
        {
            return NotFound();
        }
        List<ArticleDto> articleDTOs = new List<ArticleDto>();
        foreach (var article in articles)
        {
            articleDTOs.Add(article.AsDto());
        }
        return Ok(new { items = articleDTOs, totalCount = totalArticles });
    }

    [HttpGet]
    public async Task<IActionResult> GetPage(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 5
    )
    {
        if (pageNumber <= 0)
        {
            return NotFound();
        }
        IEnumerable<Article>? articles = await _articleRepo.GetPage(pageNumber, pageSize);
        int totalArticles = await _articleRepo.GetTotalItems();
        if (articles == null || !articles.Any())
        {
            return NotFound();
        }
        List<ArticleDto> articleDTOs = new List<ArticleDto>();
        foreach (var article in articles)
        {
            articleDTOs.Add(article.AsDto());
        }
        return Ok(new PageArticleDto(articleDTOs, totalArticles));
    }

    [Authorize]
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
        var user = await _usersRepo.GetUserById(userId);
        if (user == null)
        {
            return BadRequest();
        }
        Article artcile = articleDto.AsArticle();
        artcile.User = user;
        var created_article = await _articleRepo.Create(artcile, articleDto.ArticleTags);
        if (created_article == null)
        {
            return BadRequest();
        }
        return Ok(created_article.AsDto());
    }

    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateArticleDto articleDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var userId = int.Parse(
            User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!
        );
        var user = await _usersRepo.GetUserById(userId);
        if (user == null)
        {
            return BadRequest();
        }
        var article = await _articleRepo.Get(id);
        if (article == null)
        {
            return NotFound();
        }
        if (article.UserId != user.Id)
        {
            return Forbid();
        }
        var articleToUpdate = articleDto.AsArticle();
        articleToUpdate.Id = article.Id;
        Article? updatedArticle = await _articleRepo.Update(articleToUpdate);
        if (updatedArticle == null)
        {
            return NotFound();
        }
        return Ok(updatedArticle.AsDto());
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var userId = int.Parse(
            User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!
        );
        var user = await _usersRepo.GetUserById(userId);
        if (user == null)
        {
            return BadRequest();
        }
        var article = await _articleRepo.Get(id);
        if (article == null)
        {
            return NotFound();
        }
        if (article.UserId != user.Id)
        {
            return Forbid();
        }
        Article? DeletedArticle = await _articleRepo.Delete(id);
        if (article == null)
        {
            return NotFound();
        }
        return Ok($"Deleted article with id {id}");
    }
}
