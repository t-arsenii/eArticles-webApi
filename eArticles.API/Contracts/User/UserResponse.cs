namespace eArticles.API.Contracts.User;

public record UserResponse(
    string Id,
    string FirstName,
    string LastName,
    string UserName,
    string Email,
    string PhoneNumber
);