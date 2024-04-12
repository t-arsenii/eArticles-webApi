using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using eArticles.API.Services.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eArticles.API.Controllers;
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class ArticleTypeController : ControllerBase
{
    IArticleTypeRepository _articleTypeRepo;
    public ArticleTypeController(IArticleTypeRepository articleTypeRepo)
    {
        _articleTypeRepo = articleTypeRepo;
    }
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var articleType = await _articleTypeRepo.GetById(id);
        if (articleType == null)
        {
            return NotFound();
        }
        return Ok(new ArticleTypeDto(articleType.Id, articleType.Title));
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var articleTypes = await _articleTypeRepo.GetAll();
        if (!articleTypes.Any())
        {
            return NotFound();
        }
        List<ArticleTypeDto> articleTypeDtos = new();
        foreach (var articleType in articleTypes)
        {
            articleTypeDtos.Add(new ArticleTypeDto(Id: articleType.Id, Title: articleType.Title));
        }
        return Ok(articleTypeDtos);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateArticleTypeDto createArticleTypeDto)
    {
        ArticleType articleType = new() { Title = createArticleTypeDto.Title };
        var createdArticle = await _articleTypeRepo.Create(articleType);
        if (createdArticle == null)
        {
            return BadRequest();
        }
        return Ok(new ArticleTypeDto(Id: createdArticle.Id, Title: createdArticle.Title));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateArticleTypeDto updateArticleTypeDto)
    {
        ArticleType articleType = new() { Id = id, Title = updateArticleTypeDto.Title };
        var updatedArticle = await _articleTypeRepo.Update(articleType);
        if (updatedArticle == null)
        {
            return NotFound();
        }
        return Ok(new ArticleTypeDto(Id: updatedArticle.Id, Title: updatedArticle.Title));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedArticle = await _articleTypeRepo.Delete(id);
        if (deletedArticle == null)
        {
            return NotFound();
        }
        return Ok(new ArticleTypeDto(Id: deletedArticle.Id, Title: deletedArticle.Title));
    }
}
