using Microsoft.AspNetCore.Mvc;
using GamingBlog.API.Services.Repositories;
using GamingBlog.API.Models;
using GamingBlog.API.Data.Dtos;
using GamingBlog.API.Data.Enums;
using GamingBlog.API.Extensions;
using GamingBlog.API.Data;
using Microsoft.AspNetCore.Authorization;

namespace GamingBlog.API.Controllers;

[Route("api/[controller]")]
public class ArticlesController : ControllerBase
{
    IArticlesRepository _repo;

    public ArticlesController(IArticlesRepository repository)
    {
        _repo = repository;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        Article? article = await _repo.Get(id);
        if (article == null)
        {
            return NotFound();
        }
        return Ok(article.AsDto());
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
        IEnumerable<Article>? articles = await _repo.GetPage(pageNumber, pageSize);
        if (articles == null || !articles.Any())
        {
            return NotFound();
        }
        List<ArticleDto> articleDTOs = new List<ArticleDto>();
        foreach (var article in articles)
        {
            articleDTOs.Add(article.AsDto());
        }
        return Ok(articleDTOs);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateArticleDto articleDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Article artcile = articleDto.AsArticle();
        var created_article = await _repo.Create(artcile, articleDto.ArticleTags);
        if (created_article == null)
        {
            return BadRequest();
        }
        return Ok(created_article.AsDto());
    }

    [Authorize]
    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateArticleDto articleDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Article article = articleDto.AsArticle();
        Article? updated_article = await _repo.Update(article);
        if (updated_article == null)
        {
            return NotFound();
        }
        return Ok(updated_article.AsDto());
    }

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        Article? article = await _repo.Delete(id);
        if (article == null)
        {
            return NotFound();
        }
        return Ok($"Deleted article with id {id}");
    }
}
