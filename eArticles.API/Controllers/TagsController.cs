using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using eArticles.API.Services.Repositories;
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
        ITagsRepository _tagsRepo { get; set; }
        public TagsController(ITagsRepository tagsRepo)
        {
            _tagsRepo = tagsRepo;
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateTagDto tagDto)
        {
            Tag tag = new Tag();
            tag.Title = tagDto.Title;
            var createdTag = await _tagsRepo.Create(tag);
            if (createdTag == null)
            {
                return BadRequest();
            }
            return Ok(new TagDto(id: createdTag.Id, title: createdTag.Title));
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll()
        {
            var tags = await _tagsRepo.GetAll();
            if (tags == null)
            {
                return BadRequest();
            }
            var tagDtos = new List<TagDto>();
            foreach (var tag in tags)
            {
                tagDtos.Add(new TagDto(id: tag.Id, title: tag.Title));
            }
            return Ok(tagDtos);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var tagDeleted = await _tagsRepo.Delete(id);
            if (tagDeleted == null)
            {
                return NotFound();
            }
            return Ok(new TagDto(id: tagDeleted.Id, title: tagDeleted.Title));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, UpdateTagDto updateTagDto)
        {
            var tag = new Tag() { Id = id, Title = updateTagDto.Title };
            var updatedTag = await _tagsRepo.Update(tag);
            if (updatedTag == null)
            {
                return NotFound();
            }
            return Ok(new TagDto(id: updatedTag.Id, title: updatedTag.Title));
        }

    }
}
