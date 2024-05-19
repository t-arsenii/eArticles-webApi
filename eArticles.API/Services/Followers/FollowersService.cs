using eArticles.API.Models;
using ErrorOr;

namespace eArticles.API.Services.Followers;

public class FollowersService : IFollowersService
{
    IFollowersRepository _followersRepository;

    public FollowersService(IFollowersRepository followersRepository)
    {
        _followersRepository = followersRepository;
    }

    public async Task<ErrorOr<Follower>> Create(Follower follower)
    {
        var createFollowerResult = await _followersRepository.Create(follower);
        if (createFollowerResult.IsError)
        {
            return createFollowerResult.Errors;
        }
        return createFollowerResult.Value;
    }

    public async Task<ErrorOr<Deleted>> Delete(Guid followedId, Guid followingId)
    {
        var deleteFollowerResult = await _followersRepository.Delete(followedId, followingId);
        if (deleteFollowerResult.IsError)
        {
            return deleteFollowerResult.Errors;
        }
        return Result.Deleted;
    }
}
