using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using Microsoft.AspNetCore.Identity;

namespace eArticles.API.Services.Repositories;

public interface IUsersRepository
{
    Task<IdentityResult> Create(CreateUserDto userDto);
    Task<User?> GetUserById(int id);
    Task<User?> GetUserByUserName(string userName);
    AuthenticationResponse? AuthenticateUser(User user);
    Task<bool> IsPasswordValid(User user, string password);
}
