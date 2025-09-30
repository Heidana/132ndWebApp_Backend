using _132ndWebsite.Core.Models;

namespace _132ndWebsite.Application.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
    string GenerateRefreshToken();
}