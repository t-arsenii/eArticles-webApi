namespace eArticles.API.Data.Dtos;

public record CategoryDto
(
     int Id,
     string Title
);

public record CreateCategoryDto
(
    string Title
);

public record UpdateCategoryDto
(
    string Title
);