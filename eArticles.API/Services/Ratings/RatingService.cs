﻿using eArticles.API.Models;
using eArticles.API.Persistance.Articles;
using eArticles.API.Persistance.Ratings;
using eArticles.API.Persistance.Users;
using ErrorOr;

namespace eArticles.API.Services.Ratings;

public class RatingService : IRatingService
{
    IRatingsRepository _ratingsRepository;
    IArticlesRepository _articlesRepository;
    IUsersRepository _usersRepository;

    public RatingService(IRatingsRepository ratingsRepository, IArticlesRepository articlesRepository, IUsersRepository usersRepository)
    {
        _ratingsRepository = ratingsRepository;
        _articlesRepository = articlesRepository;
        _usersRepository = usersRepository;
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
        var getRatingResult = await _ratingsRepository.GetById(id);
        if (getRatingResult.IsError)
        {
            return getRatingResult.Errors;
        }
        var rating = getRatingResult.Value;
        var deleteRatingResult = await _ratingsRepository.Delete(rating);
        if (deleteRatingResult.IsError)
        {
            return deleteRatingResult.Errors;
        }
        var getArticleResult = await _articlesRepository.GetById(rating.ArticleId);
        if (getArticleResult.IsError)
        {
            return getArticleResult.Errors;
        }
        var article = getArticleResult.Value;
        var calculateAverageRatingResult = await _ratingsRepository.CalculateAverage(article);
        if (calculateAverageRatingResult.IsError)
        {
            return calculateAverageRatingResult.Errors;
        }
        return rating;
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
        var getRatingResult = await _ratingsRepository.GetById(updateRating.Id);
        if (getRatingResult.IsError)
        {
            return getRatingResult.Errors;
        }
        var rating = getRatingResult.Value;

        rating.Value = updateRating.Value;

        var updateRatingResult = await _ratingsRepository.Update(rating);
        if (updateRatingResult.IsError)
        {
            return updateRatingResult.Errors;
        }
        var getArticleResult = await _articlesRepository.GetById(rating.ArticleId);
        if (getArticleResult.IsError)
        {
            return getArticleResult.Errors;
        }
        var article = getArticleResult.Value;
        var calculateAverageRatingResult = await _ratingsRepository.CalculateAverage(article);
        if (calculateAverageRatingResult.IsError)
        {
            return calculateAverageRatingResult.Errors;
        }
        return updateRating;
    }
    public async Task<ErrorOr<bool>> UserHasAccess(Guid userId, Guid ratingId)
    {
        var getUserResult = await _usersRepository.GetUserById(userId);
        if (getUserResult.IsError)
        {
            return getUserResult.Errors;
        }
        var getRatingResult = await _ratingsRepository.GetById(ratingId);
        if (getRatingResult.IsError)
        {
            return getRatingResult.Errors;
        }
        var rating = getRatingResult.Value;
        var user = getUserResult.Value;
        if (rating.UserId == user.Id)
        {
            return true;
        }
        return false;
    }

}
