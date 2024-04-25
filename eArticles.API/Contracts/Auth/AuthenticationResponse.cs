namespace eArticles.API.Contracts.Auth;

public record AuthenticationResponse(string Token, DateTime Expiration);
