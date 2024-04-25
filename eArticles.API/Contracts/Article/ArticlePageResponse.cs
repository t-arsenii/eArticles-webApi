namespace eArticles.API.Contracts.Article;

public sealed record ArticlePageResponse(
    IEnumerable<ArticleResponse> items,
    int totalCount
);
