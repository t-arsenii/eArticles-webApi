using eArticles.API.Contracts.Auth;
using eArticles.API.Models;
using eArticles.API.Persistance;
using ErrorOr;

namespace eArticles.API.Services;

public class UserService : IUserService
{
    IUsersRepository _usersRepository;

    public UserService(IUsersRepository usersRepository)
    {
        _usersRepository = usersRepository;
    }

    public async Task<ErrorOr<bool>> AddUserRole(User user, string role)
    {
        var addUserRoleResult = await _usersRepository.AddUserRole(user, role);
        if (addUserRoleResult.IsError)
        {
            return addUserRoleResult.Errors;
        }
        return addUserRoleResult.Value;
    }

    public async Task<ErrorOr<AuthenticationResponse>> AuthenticateUser(User userData)
    {
        var authenticateUserResult = await _usersRepository.AuthenticateUser(userData);
        if (authenticateUserResult.IsError)
        {
            return authenticateUserResult.Errors;
        }
        return authenticateUserResult.Value;
    }

    public async Task<ErrorOr<User>> Create(User user, string password)
    {
        var createUserResult = await _usersRepository.Create(user, password);
        if (createUserResult.IsError)
        {
            return createUserResult.Errors;
        }
        return createUserResult.Value;
    }

    public async Task<ErrorOr<User>> Delete(Guid id)
    {
        var deleteUserResult = await _usersRepository.Delete(id);
        if (deleteUserResult.IsError)
        {
            return deleteUserResult.Errors;
        }
        return deleteUserResult.Value;
    }

    public async Task<ErrorOr<User>> GetUserById(Guid id)
    {
        var getUserResult = await _usersRepository.GetUserById(id);
        if (getUserResult.IsError)
        {
            return getUserResult.Errors;
        }
        return getUserResult.Value;
    }

    public async Task<ErrorOr<User>> GetUserByUserName(string userName)
    {
        var getUserResult = await _usersRepository.GetUserByUserName(userName);
        if (getUserResult.IsError)
        {
            return getUserResult.Errors;
        }
        return getUserResult.Value;
    }

    public async Task<ErrorOr<bool>> IsPasswordValid(User user, string password)
    {
        var checkPasswordResult = await _usersRepository.IsPasswordValid(user, password);
        if (checkPasswordResult.IsError)
        {
            return checkPasswordResult.Errors;
        }
        return checkPasswordResult.Value;
    }

    public async Task<ErrorOr<User>> Update(User user)
    {
        var updateUserResult = await _usersRepository.Update(user);
        if (updateUserResult.IsError)
        {
            return updateUserResult.Errors;
        }
        return updateUserResult.Value;
    }
}
