namespace _132ndWebsite.Application.Dtos;

public record RegisterUserDto(string Username, string Email, string Password);

public record LoginUserDto(string Email, string Password);

public record AuthResponseDto(string AccessToken, string RefreshToken);