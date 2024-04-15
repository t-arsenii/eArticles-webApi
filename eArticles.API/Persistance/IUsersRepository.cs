using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using Microsoft.AspNetCore.Identity;

namespace eArticles.API.Persistance;

public interface IUsersRepository
{
    Task<IdentityResult> Create(CreateUserDto userDto);
    Task<User?> GetUserById(int id);
    Task<User?> GetUserByUserName(string userName);
    Task<AuthenticationResponse?> AuthenticateUser(User userData);
    Task<bool> IsPasswordValid(User user, string password);
    Task<IdentityResult> AddUserRole(User user, string role);
}
