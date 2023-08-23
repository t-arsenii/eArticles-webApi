using GamingBlog.API.Data.Dtos;
using GamingBlog.API.Models;
using Microsoft.AspNetCore.Identity;

namespace GamingBlog.API.Services.Repositories;

public class UsersRepository : IUsersRepository
{
    private UserManager<User> _userManager;
    private JwtService _jwtService;

    public UsersRepository(UserManager<User> userManager, JwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public AuthenticationResponse? AuthenticateUser(User userData)
    {
        return _jwtService.CreateToken(userData);
    }

    public async Task<IdentityResult> Create(CreateUserDto userData)
    {
        return await _userManager.CreateAsync(
            new User() { UserName = userData.UserName, Email = userData.Email },
            userData.Password
        );
    }

    public async Task<User?> GetUserById(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<User?> GetUserByUserName(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<bool> IsPasswordValid(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }
}
