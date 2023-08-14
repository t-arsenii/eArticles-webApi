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

    [HttpGet("getItems")]
    public IActionResult GetPage(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber <= 0)
        {
            return NotFound();
        }
        IEnumerable<Article>? articles = _repo.GetPage(pageNumber, pageSize);
        if (articles == null || !articles.Any())
        {
            return NotFound();
        }
        List<ArticleDTO> articleDTOs = new List<ArticleDTO>();
        List<string>? tags;
        foreach(var article in articles){
            tags = _repo.GetArticleTags(article.Id);
            articleDTOs.Add(article.AsDto(tags)!);
        }
        return Ok(articleDTOs);
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
        return Ok(created_article.AsDto(article_Tags));
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
