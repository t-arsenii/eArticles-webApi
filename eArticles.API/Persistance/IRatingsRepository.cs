using eArticles.API.Models;
using ErrorOr;

namespace eArticles.API.Persistance;

public interface IRatingsRepository
{
    public Task<ErrorOr<Rating>> Create(Rating newRating);
    public Task<ErrorOr<Rating>> GetById(Guid id);
    public Task<ErrorOr<Rating>> Update(Rating updateRating);
    public Task<ErrorOr<Rating>> Delete(Guid id);
    public Task<ErrorOr<IEnumerable<Rating>>> GetUserRatings(Guid userId);
    public Task<bool> UserHasRating(Guid userId, Guid articleId);
    public Task<ErrorOr<Updated>> CalculateAverage(Article article);
}
