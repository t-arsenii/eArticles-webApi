using System.ComponentModel.DataAnnotations;

namespace eArticles.API.Contracts.Rating;

public record CreateRatingRequest(
    [Required] Guid articleId,
    [Required] int value
);
