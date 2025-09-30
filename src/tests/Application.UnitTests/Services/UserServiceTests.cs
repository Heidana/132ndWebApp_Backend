using _132ndWebsite.Application.Dtos;
using _132ndWebsite.Application.Services;
using _132ndWebsite.Core.Models;
using _132ndWebsite.Infrastructure.Repositories;
using Moq;

namespace _132ndWebsite.Tests.Application.Services;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _mockUserRepository;
    private readonly UserService _userService;

    public UserServiceTests()
    {
        _mockUserRepository = new Mock<IUserRepository>();
        _userService = new UserService(_mockUserRepository.Object);
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldReturnNewUser_WhenEmailAndUsernameAreUnique()
    {
        // Arrange
        var registerDto = new RegisterUserDto("testuser", "test@example.com", "Password123");
        _mockUserRepository.Setup(r => r.GetByEmailAsync(It.IsAny<string>())).ReturnsAsync((User?)null);
        _mockUserRepository.Setup(r => r.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((User?)null);

        // Act
        var (user, errorMessage) = await _userService.RegisterUserAsync(registerDto);

        // Assert
        Assert.NotNull(user);
        Assert.Equal(string.Empty, errorMessage);
        Assert.Equal(registerDto.Username, user.Username);
        _mockUserRepository.Verify(r => r.AddAsync(It.IsAny<User>()), Times.Once);
    }

    [Fact]
    public async Task RegisterUserAsync_ShouldReturnError_WhenEmailIsInUse()
    {
        // Arrange
        var registerDto = new RegisterUserDto("testuser", "test@example.com", "Password123");
        _mockUserRepository.Setup(r => r.GetByEmailAsync(registerDto.Email)).ReturnsAsync(new User { Email = registerDto.Email, Username = "anotheruser", PasswordHash = "hash" });

        // Act
        var (user, errorMessage) = await _userService.RegisterUserAsync(registerDto);

        // Assert
        Assert.Null(user);
        Assert.Equal("Email address is already in use.", errorMessage);
    }
    
    [Fact]
    public async Task GetUserByIdAsync_ShouldReturnUser_WhenUserExists()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var expectedUser = new User { Id = userId, Username = "existinguser", Email = "exists@example.com", PasswordHash = "hash"};
        _mockUserRepository.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(expectedUser);

        // Act
        var result = await _userService.GetUserByIdAsync(userId);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(userId, result.Id);
    }
}