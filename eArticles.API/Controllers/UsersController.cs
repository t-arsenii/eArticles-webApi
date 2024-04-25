using eArticles.API.Contracts.Auth;
using eArticles.API.Contracts.User;
using eArticles.API.Models;
using eArticles.API.Persistance;
using eArticles.API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace eArticles.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    IUserService _userService;
    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserRequest createUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = new User()
        {
            FirstName = createUserDto.FirstName,
            LastName = createUserDto.LastName,
            UserName = createUserDto.UserName,
            Email = createUserDto.Email,
            PhoneNumber = createUserDto.PhoneNumber,
        };
        var createUserResult = await _userService.Create(user, createUserDto.Password);
        if (createUserResult.IsError)
        {
            return BadRequest(createUserResult.FirstError.Description);
        }
        var getUserResult = await _userService.GetUserByUserName(createUserDto.UserName);
        if (getUserResult.IsError)
        {
            return BadRequest("User not found after creation.");
        }
        var addRoleResult = await _userService.AddUserRole(user, "User");
        if (addRoleResult.IsError)
        {
            return BadRequest(addRoleResult.FirstError.Description);
        }
        var createdUser = getUserResult.Value;
        var userResponse = new UserResponse(Id: createdUser.Id.ToString(),
                              FirstName: createdUser.FirstName,
                              LastName: createdUser.LastName,
                              UserName: createdUser.LastName,
                              Email: createdUser.Email,
                              PhoneNumber: createdUser.PhoneNumber);

        return CreatedAtAction(actionName: (nameof(GetById)), routeValues: new { id = userResponse.Id }, value: userResponse);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet]
    public async Task<IActionResult> GetByToken()
    {
        Guid userId;
        if (!Guid.TryParse(
           User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value,
           out userId
       ))
        {
            return BadRequest("Wrong user id format");
        }
        var getUserResult = await _userService.GetUserById(userId);
        if (getUserResult.IsError)
        {
            return NotFound(getUserResult.FirstError.Description);
        }
        var user = getUserResult.Value;
        return Ok(
            new UserResponse(
                user.Id.ToString(),
                user.FirstName,
                user.LastName,
                user.UserName,
                user.Email,
                user.PhoneNumber
            )
        );
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var getUserResult = await _userService.GetUserById(id);
        if (getUserResult.IsError)
        {
            return NotFound(getUserResult.FirstError.Description);
        }
        var user = getUserResult.Value;
        return Ok(
            new UserResponse(
                user.Id.ToString(),
                user.FirstName,
                user.LastName,
                user.UserName,
                user.Email,
                user.PhoneNumber
            )
        );
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetByUserName(string username)
    {
        var getUserResult = await _userService.GetUserByUserName(username);
        if (getUserResult.IsError)
        {
            return NotFound(getUserResult.FirstError.Description);
        }
        var user = getUserResult.Value;
        return Ok(
            new UserResponse(
                user.Id.ToString(),
                user.FirstName,
                user.LastName,
                user.UserName,
                user.Email,
                user.PhoneNumber
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
        var getUserResult = await _userService.GetUserByUserName(userData.UserName);
        if (getUserResult.IsError)
        {
            return NotFound("Bad credentials");
        }
        var user = getUserResult.Value;
        var isPasswordValidResult = await _userService.IsPasswordValid(user, userData.Password);
        if (isPasswordValidResult.IsError)
        {
            return Problem();
        }
        if (!isPasswordValidResult.Value)
        {
            return NotFound("Bad credentials");
        }
        var authenticationResponseResult = await _userService.AuthenticateUser(user);
        if (authenticationResponseResult.IsError)
        {
            return Problem();
        }
        return Ok(authenticationResponseResult.Value);
    }

    [HttpPost("admin/{id}")]
    public async Task<IActionResult> GiveAdminRole(Guid id)
    {
        var getUserResult = await _userService.GetUserById(id);
        if (getUserResult.IsError)
        {
            return NotFound(getUserResult.FirstError.Description);
        }
        var user = getUserResult.Value;
        var addUserRoleResult = await _userService.AddUserRole(user, "Admin");
        if (addUserRoleResult.IsError)
        {
            return Problem();
        }
        return Ok(new { message = $"{user.UserName} is now an Admin" });
    }
}
