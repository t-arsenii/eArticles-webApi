using System.ComponentModel.DataAnnotations;

namespace GamingBlog.API.Data.Dtos;

public abstract record BaseArticleDto(
    string Title,
    string Description,
    string Content,
    string Article_Type,
    string? Img_Url,
    List<string>? ArticleTags
);

public record ArticleDto(
    int Id,
    string Title,
    string Description,
    string Content,
    string Article_Type,
    string Published_Date,
    string Img_Url,
    List<string>? ArticleTags
) : BaseArticleDto(Title, Description, Content, Article_Type, Img_Url, ArticleTags);

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
    [Required] string Article_Type,
    List<string>? ArticleTags = null,
    [Url] string? Img_Url = null
) : BaseArticleDto(Title, Description, Content, Article_Type, Img_Url, ArticleTags);

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
        100,
        MinimumLength = 3000,
        ErrorMessage = "The Content must be between 5 and 100 characters."
    )]
        string Content,
    [Required] string Article_Type,
    List<string>? ArticleTags = null,
    [Url] string? Img_Url = null
) : BaseArticleDto(Title, Description, Content, Article_Type, Img_Url, ArticleTags);
