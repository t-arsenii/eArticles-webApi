namespace eArticles.API.Contracts.Rating;

public record RatingResponse(
    Guid id,
    Guid articleId,
    Guid userId,
    double value
);
