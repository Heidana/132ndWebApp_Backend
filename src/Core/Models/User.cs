using System.ComponentModel.DataAnnotations;
using _132ndWebsite.Core.Enums;

namespace _132ndWebsite.Core.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [MaxLength(50)]
        public required string Username { get; set; }

        [MaxLength(255)]
        public required string Email { get; set; }

        public required string PasswordHash { get; set; }

        public UserRole Role { get; set; } = UserRole.Registered;
        public UserStatus Status { get; set; } = UserStatus.Active;

        public string? Bio { get; set; }
        public string? AvatarUrl { get; set; }
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public DateTime? LastLogin { get; set; }
        public int FailedLoginAttempts { get; set; }

    }
}
