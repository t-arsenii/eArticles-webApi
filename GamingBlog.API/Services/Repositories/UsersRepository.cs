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

    public AuthenticationResponse? AuthenticateUser(AuthenticationRequest userData)
    {
        throw new NotImplementedException();
    }

    public async Task<IdentityResult> Create(CreateUserDto userData)
    {
        return await _userManager.CreateAsync(
            new IdentityUser() { UserName = userData.UserName, Email = userData.Email, },
            userData.Password
        );
    }

    public IdentityUser? GetUserById(string id)
    {
        throw new NotImplementedException();
    }

    public IdentityUser? GetUserByUserName(string userName)
    {
        throw new NotImplementedException();
    }
}
