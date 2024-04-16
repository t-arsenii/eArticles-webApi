using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace eArticles.API.Persistance;

public interface IUsersRepository
{
    Task<ErrorOr<User>> Create(CreateUserDto userDto);
    Task<ErrorOr<User>> GetUserById(int id);
    Task<ErrorOr<User>> GetUserByUserName(string userName);
    Task<ErrorOr<AuthenticationResponse>> AuthenticateUser(User userData);
    Task<ErrorOr<bool>> IsPasswordValid(User user, string password);
    Task<ErrorOr<bool>> AddUserRole(User user, string role);
}
