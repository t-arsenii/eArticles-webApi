using eArticles.API.Models;
using ErrorOr;

namespace eArticles.API.Persistance;

public interface IRatingsRepository
{
    public Task<ErrorOr<Rating>> Create(Rating newRating);
    public Task<ErrorOr<Rating>> GetById(Guid id);
    public Task<ErrorOr<Rating>> GetArticleRating(Guid articleId);
    public Task<ErrorOr<Rating>> Update(Rating updateRating);
    public Task<ErrorOr<Rating>> Delete(Guid id);
}
