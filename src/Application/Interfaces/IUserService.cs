using _132ndWebsite.Application.Common;
using _132ndWebsite.Application.Dtos;
using _132ndWebsite.Core.Models;

namespace _132ndWebsite.Application.Interfaces;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(Guid id);
    Task<User?> GetUserByEmailAsync(string email);
    Task<Result<User>> RegisterUserAsync(RegisterUserDto registerDto);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task SetRefreshTokenAsync(User user, string refreshToken, DateTime expiryTime);
}