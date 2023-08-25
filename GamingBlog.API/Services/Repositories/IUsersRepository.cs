using GamingBlog.API.Data.Dtos;
using GamingBlog.API.Models;
using Microsoft.AspNetCore.Identity;

namespace GamingBlog.API.Services.Repositories;

public interface IUsersRepository
{
    Task<IdentityResult> Create(CreateUserDto userDto);
    Task<User?> GetUserById(int id);
    Task<User?> GetUserByUserName(string userName);
    AuthenticationResponse? AuthenticateUser(User user);
    Task<bool> IsPasswordValid(User user, string password);
}
