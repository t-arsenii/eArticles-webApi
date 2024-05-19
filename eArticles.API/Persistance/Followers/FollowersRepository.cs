using eArticles.API.Data;
using eArticles.API.Models;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Persistance.Followers;

public class FollowersRepository : IFollowersRepository
{
    private readonly eArticlesDbContext _dbContext;

    public FollowersRepository(eArticlesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<Follower>> Create(Follower follower)
    {
        await _dbContext.Followers.AddAsync(follower);
        await _dbContext.SaveChangesAsync();
        return follower;
    }

    public async Task<ErrorOr<Deleted>> Delete(Guid followedId, Guid followingId)
    {
        var followerToDelete = await _dbContext.Followers.FindAsync(followingId, followedId);
        if (followerToDelete is null)
        {
            return Error.NotFound(
                description: $"Follower is not found (followed id: {followedId}, followingId: {followingId})"
            );
        }
        _dbContext.Followers.Remove(followerToDelete);
        await _dbContext.SaveChangesAsync();
        return Result.Deleted;
    }

    public async Task<ErrorOr<Follower>> Get(Guid followerId)
    {
        var follower = await _dbContext.Followers.FindAsync(followerId);
        if (follower is null)
        {
            return Error.NotFound($"Follower is not found(followerId: {followerId})");
        }
        return follower;
    }

    public async Task<ErrorOr<Follower>> GetByUsers(Guid followedId, Guid followingId)
    {
        var follower = await _dbContext.Followers
            .Where(e => e.FollowedUserId == followedId)
            .Where(e => e.FollowingUserId == followingId)
            .FirstOrDefaultAsync();
        if (follower is null)
        {
            return Error.NotFound(
                $"Follower is not found(followedId: {followedId}, followingId: {followingId})"
            );
        }
        return follower;
    }

    public async Task<ErrorOr<IEnumerable<Follower>>> GetFollowers(Guid userId)
    {
        var followersList = await _dbContext.Followers
            .Where(e => e.FollowingUserId == userId)
            .ToListAsync();
        if (followersList is null || !followersList.Any())
        {
            return Error.NotFound(description: $"No followers for user(userId: {userId})");
        }
        return followersList;
    }

    public async Task<int> GetNumberOfFollowers(Guid userId)
    {
        int followersCount = await _dbContext.Followers
            .Where(e => e.FollowingUserId == userId)
            .CountAsync();
        return followersCount;
    }
}
