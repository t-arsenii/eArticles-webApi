using System.ComponentModel.DataAnnotations;

namespace GamingBlog.API.Data.Dtos;

public record AuthenticationRequest(
    [Required]
    string UserName,
    [Required]
    string Password
);
