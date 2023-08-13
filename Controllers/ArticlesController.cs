using Microsoft.AspNetCore.Mvc;
using GamingBlog.API.Services.Repositories;
using GamingBlog.API.Models;
using GamingBlog.API.Data.Dtos;
using GamingBlog.API.Data.Enums;
using GamingBlog.API.Extensions;

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
    public IActionResult Get(int id)
    {
        Article? article = _repo.Get(id);
        if (article == null)
        {
            return NotFound();
        }
        var tags = _repo.GetArticleTags(id);
        return Ok(article.AsDto(tags));
    }

    [HttpGet("Page{currentPage:int}")]
    public IActionResult GetPage(int currentPage = 1)
    {
        if (currentPage <= 0)
        {
            return NotFound();
        }
        IEnumerable<Article>? articles = _repo.GetPage(currentPage);
        if (articles == null || !articles.Any())
        {
            return NotFound();
        }
        return Ok(articles);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateArticleDto articleDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        ArticleType a_type = (ArticleType)Enum.Parse(typeof(ArticleType), articleDto.Article_Type);
        Article artcile = new Article()
        {
            Title = articleDto.Title,
            Description = articleDto.Description,
            Content = articleDto.Content,
            Article_type = a_type,
            Img_Url = articleDto.Img_Url!
        };
        var created_article = _repo.Create(artcile, articleDto.ArticleTags);
        if (created_article == null)
        {
            return BadRequest();
        }
        var article_Tags = _repo.GetArticleTags(created_article.Id);
        return Ok(created_article.AsDto(article_Tags ?? new List<string>()));
    }

    [HttpPut]
    public IActionResult Update([FromBody] Article article)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        _repo.Update(article);
        return Ok(article);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _repo.Delete(id);
        return Ok($"Deleted article with id {id}");
    }
}
