using System.ComponentModel.DataAnnotations;

namespace eArticles.API.Data.Dtos;
public sealed record ArticleDto(
    Guid Id,
    string Title,
    string Description,
    string Content,
    string ContentType,
    string Category,
    string PublishedDate,
    string ImgUrl,
    IEnumerable<string>? Tags,
    UserDto User
);
public sealed record PageArticleDto(
    IEnumerable<ArticleDto> items,
    int totalCount
);
public sealed record CreateArticleDto(
    [Required]
    [StringLength(
        50,
        MinimumLength = 5,
        ErrorMessage = "The Title must be between 5 and 50 characters."
    )]
        string Title,
    [StringLength(
        100,
        MinimumLength = 5,
        ErrorMessage = "The Description must be between 5 and 100 characters."
    )]
        string Description,
    [Required]
    [StringLength(
        3000,
        MinimumLength = 100,
        ErrorMessage = "The Content must be between 100 and 3000 characters."
    )]
        string Content,
    [Required] Guid ContentTypeId,
    [Required] Guid CategoryId,
    IEnumerable<Guid>? TagIds = null,
    [Url] string? Img_Url = null
);

public sealed record UpdateArticleDto(
    [Required]
    [StringLength(
        50,
        MinimumLength = 5,
        ErrorMessage = "The Title must be between 5 and 50 characters."
    )]
        string Title,
    [Required]
    [StringLength(
        100,
        MinimumLength = 5,
        ErrorMessage = "The Description must be between 5 and 100 characters."
    )]
        string Description,
    [Required]
    [StringLength(
        3000,
        MinimumLength = 100,
        ErrorMessage = "The Content must be between 100 and 3000 characters."
    )]
        string Content,
    [Required] Guid ContentTypeId,
    [Required] Guid CategoryId,
    IEnumerable<Guid>? TagIds = null,
    [Url] string? Img_Url = null
);
