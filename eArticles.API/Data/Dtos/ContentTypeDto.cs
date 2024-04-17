namespace eArticles.API.Data.Dtos;

public record ContentTypeDto
(
    int Id,
    string Title
);
public record CreateContentTypeDto
(
    string Title
);
public record UpdateContentTypeDto
(
    string Title
);
