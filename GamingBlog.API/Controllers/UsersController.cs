using GamingBlog.API.Data.Dtos;
using GamingBlog.API.Models;
using GamingBlog.API.Services;
using GamingBlog.API.Services.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace GamingBlog.API.Controllers;

[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    IUsersRepository _repo;

    public UsersController(IUsersRepository usersRepository)
    {
        _repo = usersRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto createUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        IdentityResult result = await _repo.Create(createUserDto);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok(createUserDto);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        User? user = await _repo.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(new UserDto(user.UserName!, user.Email!));
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetByUserName(string username)
    {
        User? user = await _repo.GetUserByUserName(username);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(new UserDto(user.UserName!, user.Email!));
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] AuthenticationRequest userData)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        User? user = await _repo.GetUserByUserName(userData.UserName);
        if (user == null)
        {
            return NotFound("Bad credentials");
        }
        var isPasswordValid = await _repo.IsPasswordValid(user, userData.Password);
        if (!isPasswordValid)
        {
            return NotFound("Bad credentials");
        }
        var authenticationResponse = _repo.AuthenticateUser(user);
        return Ok(authenticationResponse);
    }
}
