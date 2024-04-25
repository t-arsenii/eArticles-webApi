using System.ComponentModel.DataAnnotations;

namespace eArticles.API.Contracts.Article;

public sealed record UpdateArticleRequest(
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
    IFormFile? image = null
);
