using _132ndWebsite.Core.Enums;

namespace _132ndWebsite.Application.Dtos;

public record UserProfileDto(Guid Id, string Username, UserRole Role, string? Bio, string? AvatarUrl, DateTime JoinedAt);

public record UpdateUserRoleDto(UserRole NewRole);