using System.ComponentModel.DataAnnotations;

namespace GamingBlog.API.Data.Dtos;

public record UserDto(string UserName, string Email);

public record CreateUserDto(
    [Required] string UserName,
    [Required] [EmailAddress(ErrorMessage = "Email not valid")] string Email,
    [Required]
    [StringLength(
        30,
        MinimumLength = 8,
        ErrorMessage = "The Password must be between 8 and 30 characters."
    )]
        string Password
);
