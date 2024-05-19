using eArticles.API.Models;
using ErrorOr;

namespace eArticles.API.Services.Followers;

public interface IFollowersService
{
    public Task<ErrorOr<Follower>> Create(Follower follower);

    public Task<ErrorOr<Deleted>> Delete(Guid followedId, Guid followingId);
}
