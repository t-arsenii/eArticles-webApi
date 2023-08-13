using Microsoft.AspNetCore.Mvc;
using GamingBlog.API.Data.Repositories;
using GamingBlog.API.Models;

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
        if (article != null)
        {
            return Ok(article);
        }
        return NotFound();
    }

    [HttpGet("Page{currentPage:int}")]
    public IActionResult GetPage(int currentPage = 1)
    {
        IEnumerable<Article>? articles = _repo.GetPage(currentPage);
        if(articles == null || !articles.Any())
        {
            return NotFound();
        } 
        return Ok(articles);
    }

    [HttpPost]
    public IActionResult Create([FromBody]Article article)
    {
        _repo.Create(article);
        return Ok(article);
        
    }

    [HttpPut]
    public IActionResult Update([FromBody]Article article)
    {
        _repo.Update(article);
        return Ok(article);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _repo.Delete(id);
        return Ok($"Deleted object with id {id}");
    }
}
