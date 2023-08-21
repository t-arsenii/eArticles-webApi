using GamingBlog.API.Data.Dtos;
using Microsoft.AspNetCore.Identity;

namespace GamingBlog.API.Services.Repositories;

public interface IUsersRepository
{
    Task<IdentityResult> Create(CreateUserDto userData);
    IdentityUser? GetUserById(string id);
    IdentityUser? GetUserByUserName(string userName);
    AuthenticationResponse? AuthenticateUser(AuthenticationRequest userData);
}
