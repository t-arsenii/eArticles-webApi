using System.ComponentModel.DataAnnotations;

namespace eArticles.API.Contracts.ContentType;

public record UpdateContentTypeRequest
(
    [Required]
    string Title
);
