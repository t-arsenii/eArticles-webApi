using eArticles.API.Models;
using ErrorOr;

namespace eArticles.API.Persistance;

public interface IRatingsRepository
{
    public Task<ErrorOr<Rating>> Create(Rating rating);
    public Task<ErrorOr<Rating>> GetById(Guid id);
    public Task<ErrorOr<Updated>> Update(Rating rating);
    public Task<ErrorOr<Deleted>> Delete(Rating rating);
    public Task<ErrorOr<IEnumerable<Rating>>> GetUserRatings(Guid userId);
    public Task<bool> UserHasRating(Guid userId, Guid articleId);
    public Task<ErrorOr<Updated>> CalculateAverage(Article article);
}
