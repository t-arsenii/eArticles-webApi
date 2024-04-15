using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using eArticles.API.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eArticles.API.Controllers;
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class ContentTypeController : ControllerBase
{
    IContentTypeRepository _contentTypeRepo;
    public ContentTypeController(IContentTypeRepository articleTypeRepo)
    {
        _contentTypeRepo = articleTypeRepo;
    }
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var articleType = await _contentTypeRepo.GetById(id);
        if (articleType == null)
        {
            return NotFound();
        }
        return Ok(new ContentTypeDto(articleType.Id, articleType.Title));
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var articleTypes = await _contentTypeRepo.GetAll();
        if (!articleTypes.Any())
        {
            return NotFound();
        }
        List<ContentTypeDto> articleTypeDtos = new();
        foreach (var articleType in articleTypes)
        {
            articleTypeDtos.Add(new ContentTypeDto(Id: articleType.Id, Title: articleType.Title));
        }
        return Ok(articleTypeDtos);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateArticleTypeDto createArticleTypeDto)
    {
        ContentType articleType = new() { Title = createArticleTypeDto.Title };
        var createdArticle = await _contentTypeRepo.Create(articleType);
        if (createdArticle == null)
        {
            return BadRequest();
        }
        return Ok(new ContentTypeDto(Id: createdArticle.Id, Title: createdArticle.Title));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateArticleTypeDto updateArticleTypeDto)
    {
        ContentType articleType = new() { Id = id, Title = updateArticleTypeDto.Title };
        var updatedArticle = await _contentTypeRepo.Update(articleType);
        if (updatedArticle == null)
        {
            return NotFound();
        }
        return Ok(new ContentTypeDto(Id: updatedArticle.Id, Title: updatedArticle.Title));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deletedArticle = await _contentTypeRepo.Delete(id);
        if (deletedArticle == null)
        {
            return NotFound();
        }
        return Ok(new ContentTypeDto(Id: deletedArticle.Id, Title: deletedArticle.Title));
    }
}
