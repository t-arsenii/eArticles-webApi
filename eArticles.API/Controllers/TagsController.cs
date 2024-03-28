using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using eArticles.API.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace eArticles.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TagsController : ControllerBase
    {
        ITagsRepository _tagsRepo { get; set; }
        IUsersRepository _usersRepo { get; set; }
        public TagsController(ITagsRepository tagsRepo, IUsersRepository urersRepo)
        {
            _tagsRepo = tagsRepo;
            _usersRepo = urersRepo;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateTagDto tagDto)
        {
            Tag tag = new Tag();
            tag.Title = tagDto.Title;
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!
);
            var user = await _usersRepo.GetUserById(userId);
            if (user == null)
            {
                return BadRequest();
            }
            if(user.)
            var createdTag = await _tagsRepo.Create(tag);
            return Ok(createdTag);
        }
    }
}
