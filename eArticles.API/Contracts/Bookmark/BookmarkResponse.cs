using eArticles.API.Models;

public sealed record BookmarkResponse(
    Guid id,
    Guid userId,
    Guid articleId,
    string dateAdded
);
