using System.ComponentModel.DataAnnotations;

namespace eArticles.API.Data.Dtos;

public record AuthenticationRequest(
    [Required]
    string UserName,
    [Required]
    string Password
);
