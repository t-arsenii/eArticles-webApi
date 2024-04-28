using System.ComponentModel.DataAnnotations;

namespace eArticles.API.Contracts.Article;

public sealed record CreateArticleRequestDto(
    [Required]
    [StringLength(
        50,
        MinimumLength = 5,
        ErrorMessage = "The Title must be between 5 and 50 characters."
    )]
        string title,
    [Required]
    [StringLength(
        100,
        MinimumLength = 5,
        ErrorMessage = "The Description must be between 5 and 100 characters."
    )]
        string description,
    [Required]
    [StringLength(
        3000,
        MinimumLength = 100,
        ErrorMessage = "The Content must be between 100 and 3000 characters."
    )]
        string content,
    [Required] Guid contentTypeId,
    [Required] Guid categoryId,
    IEnumerable<Guid>? tagIds = null
);
