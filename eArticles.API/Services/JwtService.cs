using eArticles.API.Data.Dtos;
using eArticles.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eArticles.API.Services;

public class JwtService
{
    private const int EXPIRATION_DAYS = 1;

    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AuthenticationResponse CreateToken(User user)
    {
        var expiration = DateTime.UtcNow.AddDays(EXPIRATION_DAYS);

        var token = CreateJwtToken(CreateClaims(user), CreateSigningCredentials(), expiration);

        var tokenHandler = new JwtSecurityTokenHandler();

        return new AuthenticationResponse(tokenHandler.WriteToken(token), expiration);
    }

    private JwtSecurityToken CreateJwtToken(
        Claim[] claims,
        SigningCredentials credentials,
        DateTime expiration
    ) =>
        new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: expiration,
            signingCredentials: credentials
        );

    private Claim[] CreateClaims(User user) =>
        new[]
        {
            // new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.UserName!),
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Role, "User"),
            new Claim(ClaimTypes.Role, "Admin")
        };

    private SigningCredentials CreateSigningCredentials() =>
        new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!)),
            SecurityAlgorithms.HmacSha256
        );
}
