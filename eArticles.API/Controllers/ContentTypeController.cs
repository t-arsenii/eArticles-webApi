﻿using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using eArticles.API.Services;
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
    IContentTypeService _contentTypeService;
    public ContentTypeController(IContentTypeService contentTypeService)
    {
        _contentTypeService = contentTypeService;
    }
    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById([FromRoute] int id)
    {
        var getContentTypeResult = await _contentTypeService.GetById(id);
        if (getContentTypeResult.IsError)
        {
            return NotFound(getContentTypeResult.FirstError.Description);
        }
        return Ok(new ContentTypeDto(getContentTypeResult.Value.Id, getContentTypeResult.Value.Title));
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var getContentTypesResult = await _contentTypeService.GetAll();
        if (!getContentTypesResult.IsError)
        {
            return NotFound(getContentTypesResult.FirstError.Description);
        }
        List<ContentTypeDto> articleTypeDtos = new();
        foreach (var contentType in getContentTypesResult.Value)
        {
            articleTypeDtos.Add(new ContentTypeDto(Id: contentType.Id, Title: contentType.Title));
        }
        return Ok(articleTypeDtos);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateContentTypeDto createArticleTypeDto)
    {
        ContentType contentType = new() { Title = createArticleTypeDto.Title };
        var createdArticleResult = await _contentTypeService.Create(contentType);
        if (createdArticleResult.IsError)
        {
            return BadRequest(createdArticleResult.FirstError.Description);
        }
        return Ok(new ContentTypeDto(Id: createdArticleResult.Value.Id, Title: createdArticleResult.Value.Title));
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateContentTypeDto updateContentTypeDto)
    {
        ContentType contentType = new() { Id = id, Title = updateContentTypeDto.Title };
        var updateArticleResult = await _contentTypeService.Update(contentType);
        if (updateArticleResult.IsError)
        {
            return NotFound(updateArticleResult.FirstError.Description);
        }
        return Ok(new ContentTypeDto(Id: updateArticleResult.Value.Id, Title: updateArticleResult.Value.Title));
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleteArticleResult = await _contentTypeService.Delete(id);
        if (deleteArticleResult.IsError)
        {
            return NotFound(deleteArticleResult.FirstError.Description);
        }
        return Ok(new ContentTypeDto(Id: deleteArticleResult.Value.Id, Title: deleteArticleResult.Value.Title));
    }
}
