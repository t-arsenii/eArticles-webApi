using eArticles.API.Models;
using ErrorOr;

public interface IFollowersRepository
{
    public Task<ErrorOr<Follower>> Create(Follower follower);
    public Task<ErrorOr<Deleted>> Delete(Guid followedId, Guid followingdId);
    public Task<ErrorOr<Follower>> Get(Guid followerId);
    public Task<ErrorOr<Follower>> GetByUsers(Guid followedId, Guid followingId);
    public Task<ErrorOr<IEnumerable<Follower>>> GetFollowers(Guid userId);
    public Task<int> GetNumberOfFollowers(Guid userId);
}
