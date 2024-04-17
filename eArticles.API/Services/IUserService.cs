using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using ErrorOr;

namespace eArticles.API.Services;

public interface IUserService
{
    Task<ErrorOr<bool>> IsPasswordValid(User user, string password);
    Task<ErrorOr<bool>> AddUserRole(User user, string role);
    Task<ErrorOr<AuthenticationResponse>> AuthenticateUser(User userData);
    Task<ErrorOr<User>> Create(User user, string password);
    Task<ErrorOr<User>> GetUserById(int id);
    Task<ErrorOr<User>> GetUserByUserName(string userName);
    Task<ErrorOr<User>> Update(User user);
    Task<ErrorOr<User>> Delete(int id);
}
