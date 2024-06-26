﻿using eArticles.API.Contracts.ContentType;
using eArticles.API.Models;
using eArticles.API.Services.ContentTypes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eArticles.API.Controllers;

[Authorize(Roles = "Admin")]
public class ContentTypesController : ApiController
{
    IContentTypeService _contentTypeService;
    public ContentTypesController(IContentTypeService contentTypeService)
    {
        _contentTypeService = contentTypeService;
    }
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        var getContentTypeResult = await _contentTypeService.GetById(id);
        if (getContentTypeResult.IsError)
        {
            return NotFound(getContentTypeResult.FirstError.Description);
        }
        return Ok(new ContentTypeResponse(getContentTypeResult.Value.Id, getContentTypeResult.Value.Title));
    }
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll()
    {
        var getContentTypesResult = await _contentTypeService.GetAll();
        if (getContentTypesResult.IsError)
        {
            return NotFound(getContentTypesResult.FirstError.Description);
        }
        List<ContentTypeResponse> articleTypeDtos = new();
        foreach (var contentType in getContentTypesResult.Value)
        {
            articleTypeDtos.Add(new ContentTypeResponse(Id: contentType.Id, Title: contentType.Title));
        }
        return Ok(articleTypeDtos);
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateContentTypeRequest createArticleTypeDto)
    {
        ContentType contentType = new() { Title = createArticleTypeDto.Title };
        var createdArticleResult = await _contentTypeService.Create(contentType);
        if (createdArticleResult.IsError)
        {
            return BadRequest(createdArticleResult.FirstError.Description);
        }
        var contentTypeResponse = new ContentTypeResponse(Id: createdArticleResult.Value.Id, Title: createdArticleResult.Value.Title);
        return CreatedAtAction(actionName: (nameof(GetById)), routeValues: new { id = contentTypeResponse.Id }, value: contentTypeResponse);

    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateContentTypeRequest updateContentTypeDto)
    {
        ContentType contentType = new() { Id = id, Title = updateContentTypeDto.Title };
        var updateArticleResult = await _contentTypeService.Update(contentType);
        if (updateArticleResult.IsError)
        {
            return NotFound(updateArticleResult.FirstError.Description);
        }
        return NoContent();
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deleteArticleResult = await _contentTypeService.Delete(id);
        if (deleteArticleResult.IsError)
        {
            return NotFound(deleteArticleResult.FirstError.Description);
        }
        return NoContent();
    }
}
