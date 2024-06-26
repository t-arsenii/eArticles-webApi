﻿using System.ComponentModel.DataAnnotations;

namespace eArticles.API.Contracts.User;

public record CreateUserRequest(
    [Required] string FirstName,
    [Required] string LastName,
    [Required][Phone] string PhoneNumber,
    [Required] string UserName,
    [Required][EmailAddress(ErrorMessage = "Email not valid")] string Email,
    [Required]
    [StringLength(
        30,
        MinimumLength = 8,
        ErrorMessage = "The Password must be between 8 and 30 characters."
    )]
        string Password
);