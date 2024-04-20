using System.ComponentModel.DataAnnotations;

namespace eArticles.API.Data.Dtos;

public abstract record BaseArticleDto(
    string Title,
    string Description,
    string Content,
    string ContentTypeId,
    string CategoryId,
    string? ImgUrl,
    IEnumerable<string>? TagIds
);

public record ArticleDto(
    string Id,
    string Title,
    string Description,
    string Content,
    string ContentTypeId,
    string CategoryId,
    string PublishedDate,
    string ImgUrl,
    IEnumerable<string>? TagIds,
    UserDto User
) : BaseArticleDto(Title, Description, Content, ContentTypeId, CategoryId, ImgUrl, TagIds);

public record PageArticleDto(
    IEnumerable<ArticleDto> items,
    int totalCount
);
public record CreateArticleDto(
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
    [Required] string ContentTypeId,
    [Required] string CategoryId,
    IEnumerable<string>? TagIds = null,
    [Url] string? Img_Url = null
) : BaseArticleDto(Title, Description, Content, ContentTypeId, CategoryId, Img_Url, TagIds);

public record UpdateArticleDto(
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
    [Required] string ContentTypeId,
    [Required] string CategoryId,
    IEnumerable<string>? TagIds = null,
    [Url] string? Img_Url = null
) : BaseArticleDto(Title, Description, Content, ContentTypeId, CategoryId, Img_Url, TagIds);
