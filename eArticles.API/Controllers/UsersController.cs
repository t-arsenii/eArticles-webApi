using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using eArticles.API.Services.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace eArticles.API.Controllers;

[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    IUsersRepository _usersRepo;

    public UsersController(IUsersRepository usersRepository)
    {
        _usersRepo = usersRepository;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        IdentityResult result = await _usersRepo.Create(createUserDto);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok(createUserDto);
    }

    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetById()
    {
        var userId = int.Parse(
            User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value!
        );
        var user = await _usersRepo.GetUserById(userId);
        if (user == null)
        {
            return BadRequest();
        }
        return Ok(
            new UserDto(
                user.FirstName!,
                user.LastName!,
                user.UserName!,
                user.Email!,
                user.PhoneNumber!
            )
        );
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        User? user = await _usersRepo.GetUserById(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(
            new UserDto(
                user.FirstName!,
                user.LastName!,
                user.UserName!,
                user.Email!,
                user.PhoneNumber!
            )
        );
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetByUserName(string username)
    {
        User? user = await _usersRepo.GetUserByUserName(username);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(
            new UserDto(
                user.FirstName!,
                user.LastName!,
                user.UserName!,
                user.Email!,
                user.PhoneNumber!
            )
        );
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] AuthenticationRequest userData)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        User? user = await _usersRepo.GetUserByUserName(userData.UserName);
        if (user == null)
        {
            return NotFound("Bad credentials");
        }
        var isPasswordValid = await _usersRepo.IsPasswordValid(user, userData.Password);
        if (!isPasswordValid)
        {
            return NotFound("Bad credentials");
        }
        var authenticationResponse = _usersRepo.AuthenticateUser(user);
        return Ok(authenticationResponse);
    }
}
