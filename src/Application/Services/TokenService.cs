using _132ndWebsite.Application.Interfaces;
using _132ndWebsite.Core.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace _132ndWebsite.Application.Services;

public class TokenService : ITokenService
{
    private readonly SymmetricSecurityKey _key;
    private readonly string _issuer;
    private readonly string _audience;

    public TokenService(IConfiguration config)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] ?? throw new InvalidOperationException("JWT Key not found.")));
        _issuer = config["Jwt:Issuer"] ?? throw new InvalidOperationException("JWT Issuer not found.");
        _audience = config["Jwt:Audience"] ?? throw new InvalidOperationException("JWT Audience not found.");
    }

    public string CreateToken(User user)
    {
        var claims = new List<Claim>
        {
            // CORRECTED: Use ClaimTypes consistently
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(JwtRegisteredClaimNames.Name, user.Username),
            new(ClaimTypes.Role, user.Role.ToString())
        };

        var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            // UPDATED: Token lifetime reduced to 1 hour
            Expires = DateTime.UtcNow.AddHours(1),
            SigningCredentials = creds,
            Issuer = _issuer,
            Audience = _audience
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}