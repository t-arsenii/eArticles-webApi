using eArticles.API.Data;
using eArticles.API.Models;
using ErrorOr;

namespace eArticles.API.Persistance;

public class RatingsRepository : IRatingsRepository
{
    readonly eArticlesDbContext _dbContext;
    readonly IArticlesRepository _articlesRepository;
    public RatingsRepository(eArticlesDbContext dbContext, IArticlesRepository articlesRepository)
    {
        _dbContext = dbContext;
        _articlesRepository = articlesRepository;
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

    public Task<ErrorOr<Rating>> GetArticleRating(Guid articleId)
    {
        var articleResult = _articlesRepository.GetById(articleId);
        if(articleResult)
        throw new NotImplementedException();
    }


    public Task<ErrorOr<Rating>> Update(Rating updateRating)
    {
        throw new NotImplementedException();
    }
}
