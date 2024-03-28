namespace eArticles.API.Data.Dtos;

public record TagDto(
    int id,
    string title
);
public record CreateTagDto(
    string Title
);
public record UpdateTagDto(
    string Title
);