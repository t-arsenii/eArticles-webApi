using eArticles.API.Data;
using eArticles.API.Models;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace eArticles.API.Persistance.Ratings;

public class RatingsRepository : IRatingsRepository
{
    readonly eArticlesDbContext _dbContext;
    public RatingsRepository(eArticlesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ErrorOr<Rating>> Create(Rating rating)
    {
        await _dbContext.Ratings.AddAsync(rating);
        await _dbContext.SaveChangesAsync();
        return rating;
    }
    public async Task<ErrorOr<Rating>> GetById(Guid id)
    {
        var rating = await _dbContext.Ratings.Include(e => e.Article)
                                             .FirstOrDefaultAsync(e => e.Id == id);
        if (rating is null)
        {
            return Error.NotFound(description: $"Rating is not found (rating id: {id})");
        }
        return rating;
    }

    public async Task<ErrorOr<Deleted>> Delete(Rating rating)
    {
        _dbContext.Ratings.Remove(rating);
        await _dbContext.SaveChangesAsync();
        return Result.Deleted;
    }
    public async Task<ErrorOr<Updated>> Update(Rating rating)
    {
        _dbContext.Ratings.Update(rating);
        await _dbContext.SaveChangesAsync();
        return Result.Updated;
    }

    public async Task<ErrorOr<IEnumerable<Rating>>> GetUserRatings(Guid userId)
    {
        var ratings = await _dbContext.Ratings.Where(e => e.UserId == userId).ToListAsync();
        if (ratings is null || !ratings.Any())
        {
            return Error.NotFound($"Ratings are not found for user (user id: ${userId})");
        }
        return ratings;
    }

    public async Task<bool> UserHasRating(Guid userId, Guid articleId)
    {
        var rating = await _dbContext.Ratings.Where(e => e.UserId == userId)
                                       .Where(e => e.ArticleId == articleId)
                                       .FirstOrDefaultAsync();
        if (rating is null)
        {
            return false;
        }
        return true;
    }

    public async Task<ErrorOr<Updated>> CalculateAverage(Article article)
    {
        article.AverageRating = Math.Round(article.Ratings.Average(e => e.Value), 2);
        await _dbContext.SaveChangesAsync();
        return Result.Updated;
    }
}
