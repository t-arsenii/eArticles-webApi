namespace eArticles.API.Data.Dtos;

public record TagDto(
    Guid Id,
    string Title
);
public record CreateTagDto(
    string Title
);
public record UpdateTagDto(
    string Title
);