using System.ComponentModel.DataAnnotations;

namespace eArticles.API.Data.Dtos;

public abstract record BaseArticleDto(
    string Title,
    string Description,
    string Content,
    string ArticleType,
    string? ImgUrl,
    IEnumerable<string>? ArticleTags
);

public record ArticleDto(
    string Id,
    string Title,
    string Description,
    string Content,
    string ArticleType,
    string PublishedDate,
    string ImgUrl,
    IEnumerable<string>? ArticleTags,
    UserDto User
) : BaseArticleDto(Title, Description, Content, ArticleType, ImgUrl, ArticleTags);

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
        MinimumLength = 10,
        ErrorMessage = "The Content must be between 10 and 3000 characters."
    )]
        string Content,
    [Required] string ArticleType,
    IEnumerable<string>? ArticleTags = null,
    [Url] string? Img_Url = null
) : BaseArticleDto(Title, Description, Content, ArticleType, Img_Url, ArticleTags);

public record UpdateArticleDto(
    [Required]
    [StringLength(
        100,
        MinimumLength = 5,
        ErrorMessage = "The Title must be between 5 and 100 characters."
    )]
        string Title,
    [Required]
    [StringLength(
        200,
        MinimumLength = 5,
        ErrorMessage = "The Description must be between 5 and 200 characters."
    )]
        string Description,
    [Required]
    [StringLength(
        100,
        MinimumLength = 3000,
        ErrorMessage = "The Content must be between 5 and 100 characters."
    )]
        string Content,
    [Required] string ArticleType,
    IEnumerable<string>? ArticleTags = null,
    [Url] string? Img_Url = null
) : BaseArticleDto(Title, Description, Content, ArticleType, Img_Url, ArticleTags);
