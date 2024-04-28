using System.ComponentModel.DataAnnotations;

namespace eArticles.API.Contracts.Article;

public sealed record CreateArticleRequest(
    [Required] string json,
    [Required] IFormFile image
);
