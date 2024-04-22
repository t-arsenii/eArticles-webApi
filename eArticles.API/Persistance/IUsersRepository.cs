using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using ErrorOr;
using Microsoft.AspNetCore.Identity;

namespace eArticles.API.Persistance;

public interface IUsersRepository
{
    Task<ErrorOr<User>> Create(User user, string password);
    Task<ErrorOr<User>> GetUserById(Guid id);
    Task<ErrorOr<User>> GetUserByUserName(string userName);
    Task<ErrorOr<User>> Update(User user);
    Task<ErrorOr<User>> Delete(Guid id);
    Task<ErrorOr<bool>> IsPasswordValid(User user, string password);
    Task<ErrorOr<bool>> AddUserRole(User user, string role);
    Task<ErrorOr<AuthenticationResponse>> AuthenticateUser(User userData);
}
