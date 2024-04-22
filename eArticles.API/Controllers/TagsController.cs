using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using eArticles.API.Persistance;
using eArticles.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eArticles.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Authorize(Roles = "Admin")]
    public class TagsController : ControllerBase
    {
        ITagsService _tagsService;

        public TagsController(ITagsService tagsService)
        {
            _tagsService = tagsService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTagDto tagDto)
        {
            Tag tag = new Tag();
            tag.Title = tagDto.Title;
            var createTagResult = await _tagsService.Create(tag);
            if (createTagResult.IsError)
            {
                return BadRequest(createTagResult.FirstError.Description);
            }
            var createdTag = createTagResult.Value;
            var tagResponse = new TagDto(Id: createdTag.Id, Title: createdTag.Title);
            return CreatedAtAction(actionName: (nameof(GetById)), routeValues: new { id = tagResponse.Id }, value: tagResponse);

        }
        [HttpGet("{id:guid}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var getTagResult = await _tagsService.GetById(id);
            if (getTagResult.IsError)
            {
                return NotFound(getTagResult.FirstError.Description);
            }
            var tag = getTagResult.Value;
            return Ok(new TagDto(Id: tag.Id, Title: tag.Title));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var getTagsResult = await _tagsService.GetAll();
            if (getTagsResult.IsError)
            {
                return NotFound(getTagsResult.FirstError.Description);
            }
            var tagDtos = new List<TagDto>();
            var tags = getTagsResult.Value;
            foreach (var tag in tags)
            {
                tagDtos.Add(new TagDto(Id: tag.Id, Title: tag.Title));
            }
            return Ok(tagDtos);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteTagResult = await _tagsService.Delete(id);
            if (deleteTagResult.IsError)
            {
                return NotFound(deleteTagResult.FirstError.Description);
            }
            var deletedTag = deleteTagResult.Value;
            return NoContent();
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, UpdateTagDto updateTagDto)
        {
            var tag = new Tag() { Id = id, Title = updateTagDto.Title };
            var updateTagResult = await _tagsService.Update(tag);
            if (updateTagResult.IsError)
            {
                return NotFound(updateTagResult.FirstError.Description);
            }
            var updatedTag = updateTagResult.Value; 
            return NoContent();
        }

    }
}
