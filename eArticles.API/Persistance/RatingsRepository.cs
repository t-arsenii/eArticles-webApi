using eArticles.API.Data;
using eArticles.API.Models;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Persistance;

public class RatingsRepository : IRatingsRepository
{
    readonly eArticlesDbContext _dbContext;
    public RatingsRepository(eArticlesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<Rating>> Create(Rating newRating)
    {
        await _dbContext.Ratings.AddAsync(newRating);
        await _dbContext.SaveChangesAsync();
        return newRating;
    }
    public async Task<ErrorOr<Rating>> GetById(Guid id)
    {
        var rating = await _dbContext.Ratings.FindAsync(id);
        if (rating is null)
        {
            return Error.NotFound(description: $"Rating is not found (rating id: {id})");
        }
        return rating;
    }

    public async Task<ErrorOr<Rating>> Delete(Guid id)
    {
        var rating = await _dbContext.Ratings.FindAsync(id);
        if (rating is null)
        {
            return Error.NotFound(description: $"Rating is not found (rating id: {id})");
        }
        _dbContext.Ratings.Remove(rating);
        await _dbContext.SaveChangesAsync();
        return rating;
    }
    public async Task<ErrorOr<Rating>> Update(Rating updateRating)
    {
        _dbContext.Ratings.Update(updateRating);
        await _dbContext.SaveChangesAsync();
        return updateRating;
    }

    public async Task<ErrorOr<IEnumerable<Rating>>> GetUserRatings(Guid userId)
    {
        var ratings = await _dbContext.Ratings.Where(e => e.UserId == userId).ToListAsync();
        if(ratings is null || !ratings.Any())
        {
            return Error.NotFound($"Ratings are not found for user (user id: ${userId})");
        }
        return ratings;
    }

    public async Task<bool> HasRating(Guid userId, Guid articleId)
    {
        var rating = await _dbContext.Ratings.Where(e => e.UserId == userId)
                                       .Where(e => e.ArticleId == articleId)
                                       .FirstOrDefaultAsync();
        if(rating is null)
        {
            return false;
        }
        return true;
    }
}
