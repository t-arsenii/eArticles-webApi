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
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateTagDto tagDto)
        {
            Tag tag = new Tag();
            tag.Title = tagDto.Title;
            var createdTag = await _tagsRepo.Create(tag);
            return Ok(createdTag);
        }
    }
}
