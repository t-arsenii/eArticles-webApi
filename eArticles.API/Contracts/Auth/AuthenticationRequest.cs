using System.ComponentModel.DataAnnotations;

namespace eArticles.API.Contracts.Auth;

public record AuthenticationRequest(
    [Required]
    string UserName,
    [Required]
    string Password
);
