using eArticles.API.Contracts.Auth;
using eArticles.API.Models;
using eArticles.API.Services;
using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace eArticles.API.Persistance.Users;

public class UsersRepository : IUsersRepository
{
    private UserManager<User> _userManager;
    private JwtService _jwtService;

    public UsersRepository(UserManager<User> userManager, JwtService jwtService)
    {
        _userManager = userManager;
        _jwtService = jwtService;
    }

    public async Task<ErrorOr<AuthenticationResponse>> AuthenticateUser(User userData)
    {
        return await _jwtService.CreateTokenAsync(userData);
    }

    public async Task<ErrorOr<User>> Create(User user, string password)
    {
        var createUserResult = (await _userManager.CreateAsync(user, password)).ToErrorOr();
        if (createUserResult.IsError)
        {
            return createUserResult.Errors;
        }
        return user;
    }

    public async Task<ErrorOr<User>> GetUserById(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            return Error.NotFound($"User is not found (user id: {id})");
        }
        return user;
    }

    public async Task<ErrorOr<User>> GetUserByUserName(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is null)
        {
            return Error.NotFound(description: $"User is not found (userName: {userName})");
        }
        return user;
    }

    public async Task<ErrorOr<bool>> IsPasswordValid(User user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<ErrorOr<bool>> AddUserRole(User user, string role)
    {
        var addRoleResult = (await _userManager.AddToRoleAsync(user, role)).ToErrorOr();
        if (addRoleResult.IsError)
        {
            return addRoleResult.Errors;
        }
        return true;
    }

    public Task<ErrorOr<User>> Update(User user)
    {
        throw new NotImplementedException();
    }

    public Task<ErrorOr<User>> Delete(Guid userId)
    {
        throw new NotImplementedException();
    }
}
