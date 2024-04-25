using System.ComponentModel.DataAnnotations;

namespace eArticles.API.Contracts.ContentType;

public record CreateContentTypeRequest
(
    [Required]
    string Title
);
