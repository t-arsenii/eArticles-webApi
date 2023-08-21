using Microsoft.AspNetCore.Mvc;
using GamingBlog.API.Services.Repositories;
using GamingBlog.API.Models;
using GamingBlog.API.Data.Dtos;
using GamingBlog.API.Data.Enums;
using GamingBlog.API.Extensions;
using GamingBlog.API.Data;

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
        return Ok(article.AsDto());
    }

    [HttpGet]
    public IActionResult GetPage([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5)
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
        List<ArticleDto> articleDTOs = new List<ArticleDto>();
        foreach (var article in articles)
        {
            articleDTOs.Add(article.AsDto());
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
        Article artcile = CreateArticleFromDto(articleDto);
        var created_article = _repo.Create(artcile, articleDto.ArticleTags);
        if (created_article == null)
        {
            return BadRequest();
        }
        return Ok(created_article.AsDto());
    }

    [HttpPut]
    public IActionResult Update([FromBody] UpdateArticleDto articleDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        Article article = CreateArticleFromDto(articleDto);
        Article? updated_article = _repo.Update(article);
        if (updated_article == null)
        {
            return NotFound();
        }
        return Ok(updated_article.AsDto());
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        Article? article = _repo.Delete(id);
        if (article == null)
        {
            return NotFound();
        }
        return Ok($"Deleted article with id {id}");
    }

    private Article CreateArticleFromDto(BaseArticleDto articleDTO)
    {
        ArticleType a_type = (ArticleType)Enum.Parse(typeof(ArticleType), articleDTO.Article_Type);
        return new Article()
        {
            Title = articleDTO.Title,
            Description = articleDTO.Description,
            Content = articleDTO.Content,
            Article_type = a_type,
            Img_Url = articleDTO.Img_Url!
        };
    }
}
