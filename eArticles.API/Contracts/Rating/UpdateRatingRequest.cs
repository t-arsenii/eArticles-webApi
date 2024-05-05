using System.ComponentModel.DataAnnotations;

namespace eArticles.API.Contracts.Rating;

public record UpdateRatingRequest
(
    [Required] int value
);
