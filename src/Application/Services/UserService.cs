using _132ndWebsite.Application.Common;
using _132ndWebsite.Application.Dtos;
using _132ndWebsite.Application.Interfaces;
using _132ndWebsite.Core.Models;
using _132ndWebsite.Infrastructure.Repositories;

namespace _132ndWebsite.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }

    public async Task<Result<User>> RegisterUserAsync(RegisterUserDto registerDto)
    {
        if (await _userRepository.GetByEmailAsync(registerDto.Email) is not null)
        {
            return Result<User>.Failure("Email address is already in use.");
        }

        if (await _userRepository.GetByUsernameAsync(registerDto.Username) is not null)
        {
            return Result<User>.Failure("Username is already taken.");
        }

        var newUser = new User
        {
            Id = Guid.NewGuid(),
            Username = registerDto.Username,
            Email = registerDto.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
        };

        await _userRepository.AddAsync(newUser);
        // COMMIT THE TRANSACTION
        await _userRepository.SaveChangesAsync();

        return Result<User>.Success(newUser);
    }

    public Task<bool> CheckPasswordAsync(User user, string password)
    {
        return Task.FromResult(BCrypt.Net.BCrypt.Verify(password, user.PasswordHash));
    }

    public async Task SetRefreshTokenAsync(User user, string refreshToken, DateTime expiryTime)
    {
        user.RefreshToken = refreshToken;
        user.RefreshTokenExpiryTime = expiryTime;
        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync();
    }
}