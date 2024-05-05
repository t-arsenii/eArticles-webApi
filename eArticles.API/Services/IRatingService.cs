using eArticles.API.Models;
using ErrorOr;

namespace eArticles.API.Services;

public interface IRatingService
{
    public Task<ErrorOr<Rating>> Create(Rating newRating);
    public Task<ErrorOr<Rating>> GetById(Guid id);
    public Task<ErrorOr<double>> GetArticleAverageRating(Guid articleId);
    public Task<ErrorOr<Rating>> Update(Rating updateRating);
    public Task<ErrorOr<Rating>> Delete(Guid id);
    Task<ErrorOr<bool>> UserHasAccess(Guid userId, Guid ratingId);
}
