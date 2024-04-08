using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using Microsoft.AspNetCore.Identity;

namespace eArticles.API.Services.Repositories;

public class UsersRepository : IUsersRepository
{
    private UserManager<User> _userManager;
    private JwtService _jwtService;

    public UsersRepository(UserManager<User> userManager, JwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<AuthenticationResponse?> AuthenticateUser(User userData)
    {
        return await _jwtService.CreateTokenAsync(userData);
    }

    public async Task<IdentityResult> Create(CreateUserDto userData)
    {
        return await _userManager.CreateAsync(
            new User()
            {
                UserName = userData.UserName,
                Email = userData.Email,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                PhoneNumber = userData.PhoneNumber
            },
            userData.Password
        );
    }

    public async Task<User?> GetUserById(int id)
    {
        return await _userManager.FindByIdAsync(id.ToString());
    }

    public async Task<User?> GetUserByUserName(string userName)
    {
        return await _userManager.FindByNameAsync(userName);
    }

    public async Task<bool> IsPasswordValid(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IdentityResult> AddUserRole(User user, string role)
    {
        return await _userManager.AddToRoleAsync(user, role);
    }
}
