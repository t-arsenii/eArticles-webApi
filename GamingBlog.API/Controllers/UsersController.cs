using GamingBlog.API.Data.Dtos;
using GamingBlog.API.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace GamingBlog.API.Controllers;

[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private UserManager<IdentityUser> _userManager;
    private JwtService _jwtService;

    public UsersController(UserManager<IdentityUser> userManager, JwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUserDto createUserDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _userManager.CreateAsync(
            new IdentityUser() { UserName = createUserDto.UserName, Email = createUserDto.Email, },
            createUserDto.Password
        );
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }
        return Ok(createUserDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(string id)
    {
        IdentityUser? user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(new UserDto(user.UserName!, user.Email!));
    }

    [HttpGet("{username}")]
    public async Task<IActionResult> GetByUserName(string username)
    {
        IdentityUser? user = await _userManager.FindByNameAsync(username);
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
        IdentityUser? user = await _userManager.FindByNameAsync(userData.UserName);
        if (user == null)
        {
            return NotFound("Bad credentials");
        }
        var isPasswordValid = await _userManager.CheckPasswordAsync(user, userData.Password);
        if(!isPasswordValid)
        {
            return NotFound("Bad credentials");
        }
        var authenticationResponse = _jwtService.CreateToken(user);
        return Ok(authenticationResponse);
    }
}
