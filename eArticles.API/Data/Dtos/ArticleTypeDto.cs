namespace eArticles.API.Data.Dtos;

public record ArticleTypeDto
(
    int Id,
    string Title
);
public record CreateArticleTypeDto
(
    string Title
);
public record UpdateArticleTypeDto
(
    int Id,
    string Title
);
