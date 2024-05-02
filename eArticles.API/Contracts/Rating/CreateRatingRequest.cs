namespace eArticles.API.Contracts.Rating;

public record CreateRatingRequest(
    Guid articleId,
    int value
);
