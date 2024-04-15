namespace eArticles.API.Data.Dtos;

public record ContentTypeDto
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
    string Title
);
