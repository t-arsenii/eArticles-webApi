using GamingBlog.API.Data.Dtos;
using Microsoft.AspNetCore.Identity;

namespace GamingBlog.API.Services.Repositories;

public interface IUsersRepository
{
    Task<IdentityResult> Create(CreateUserDto userDto);
    Task<IdentityUser?> GetUserById(string id);
    Task<IdentityUser?> GetUserByUserName(string userName);
    AuthenticationResponse? AuthenticateUser(IdentityUser user);
    Task<bool> IsPasswordValid(IdentityUser user, string password);
}
