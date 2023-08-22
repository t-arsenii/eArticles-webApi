using GamingBlog.API.Data.Dtos;
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
    IUsersRepository _usersRepository;

    public UsersController(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto createUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _usersRepository.Create(createUserDto);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok(createUserDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(string id)
    {
        IdentityUser? user = await _usersRepository.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(new UserDto(user.UserName!, user.Email!));
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetByUserName(string username)
    {
        IdentityUser? user = await _usersRepository.GetUserByUserName(username);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(new UserDto(user.UserName!, user.Email!));
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody]AuthenticationRequest userData)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest(ModelState);

        }
        IdentityUser? user = await _usersRepository.GetUserByUserName(userData.UserName);
        if (user == null)
        {
            return NotFound("Bad credentials");
        }
        var isPasswordValid = await _usersRepository.IsPasswordValid(user, userData.Password);
        if(!isPasswordValid)
        {
            return NotFound("Bad credentials");
        }
        var authenticationResponse = _usersRepository.AuthenticateUser(user);
        return Ok(authenticationResponse);
    }
}
