namespace eArticles.API.Data.Dtos;

public record AuthenticationResponse(string Token, DateTime Expiration);
