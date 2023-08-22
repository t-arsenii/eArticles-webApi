using GamingBlog.API.Data.Dtos;
using Microsoft.AspNetCore.Identity;

namespace GamingBlog.API.Services.Repositories;

public class UsersRepository : IUsersRepository
{
    private UserManager<IdentityUser> _userManager;
    private JwtService _jwtService;

    public UsersRepository(UserManager<IdentityUser> userManager, JwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public AuthenticationResponse? AuthenticateUser(IdentityUser userData)
    {
        return _jwtService.CreateToken(userData);
    }

    public async Task<IdentityResult> Create(CreateUserDto userData)
    {
        return await _userManager.CreateAsync(
            new IdentityUser() { UserName = userData.UserName, Email = userData.Email, },
            userData.Password
        );
    }

    public async Task<IdentityUser?> GetUserById(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<IdentityUser?> GetUserByUserName(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }
    public async Task<bool> IsPasswordValid(IdentityUser user, string password){
        return await _userManager.CheckPasswordAsync(user, password);
    }
}
