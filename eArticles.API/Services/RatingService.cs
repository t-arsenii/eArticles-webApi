using eArticles.API.Models;
using eArticles.API.Persistance;
using ErrorOr;

namespace eArticles.API.Services;

public class RatingService : IRatingService
{
    IRatingsRepository _ratingsRepository;
    IArticlesRepository _articlesRepository;

    public RatingService(IRatingsRepository ratingsRepository, IArticlesRepository articlesRepository)
    {
        _ratingsRepository = ratingsRepository;
        _articlesRepository = articlesRepository;
    }

    public async Task<ErrorOr<Rating>> Create(Rating newRating)
    {
        var getArticleResult = await _articlesRepository.GetById(newRating.ArticleId);
        if (getArticleResult.IsError)
        {
            return getArticleResult.Errors;
        }
        var article = getArticleResult.Value;
        if (article.UserId == newRating.UserId)
        {
            return Error.Unexpected($"User can't rate his own article");
        }
        if (await _ratingsRepository.UserHasRating(newRating.UserId, newRating.ArticleId))
        {
            return Error.Unexpected(description: $"User already has a rating for the article (articleId: ${newRating.ArticleId})");
        }
        var createRatingResult = await _ratingsRepository.Create(newRating);
        if (createRatingResult.IsError)
        {
            return createRatingResult.Errors;
        }
        var calculateAverageRatingResult = await _ratingsRepository.CalculateAverage(article);
        if (calculateAverageRatingResult.IsError)
        {
            return calculateAverageRatingResult.Errors;
        }
        return createRatingResult;
    }

    public async Task<ErrorOr<Rating>> Delete(Guid id)
    {
        var deleteRatingResult = await _ratingsRepository.Delete(id);
        if (deleteRatingResult.IsError)
        {
            return deleteRatingResult.Errors;
        }
        return deleteRatingResult;
    }

    public async Task<ErrorOr<double>> GetArticleAverageRating(Guid articleId)
    {
        var getArticleResult = await _articlesRepository.GetById(articleId);
        if (getArticleResult.IsError)
        {
            return getArticleResult.Errors;
        }
        var article = getArticleResult.Value;
        return article.AverageRating;
    }

    public async Task<ErrorOr<Rating>> GetById(Guid id)
    {
        var getRatingResult = await _ratingsRepository.GetById(id);
        if (getRatingResult.IsError)
        {
            return getRatingResult.Errors;
        }
        return getRatingResult.Value;
    }

    public async Task<ErrorOr<Rating>> Update(Rating updateRating)
    {
        var updateArticleResult = await _ratingsRepository.Update(updateRating);
        if (updateArticleResult.IsError)
        {
            return updateArticleResult.Errors;
        }
        return updateArticleResult.Value;
    }
}
