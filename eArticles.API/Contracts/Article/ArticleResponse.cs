
using eArticles.API.Contracts.User;

namespace eArticles.API.Contracts.Article;

public sealed record ArticleResponse(
    Guid Id,
    string Title,
    string Description,
    string Content,
    string ContentType,
    string Category,
    string PublishedDate,
    string ImgName,
    double AverageRating,
    int viewsCount,
    IEnumerable<string>? Tags,
    UserResponse User
);
