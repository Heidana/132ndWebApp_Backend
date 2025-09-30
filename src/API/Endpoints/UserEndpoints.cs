using _132ndWebsite.Application.Dtos;
using _132ndWebsite.Application.Interfaces;
using _132ndWebsite.Core.Enums;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace _132ndWebsite.API.Endpoints;

public static class UserEndpoints
{
    public static void MapUserEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api").WithTags("Auth & Users");
        var authGroup = group.MapGroup("/auth");
        var userGroup = group.MapGroup("/users");

        // POST /api/auth/register
        authGroup.MapPost("/register", async (IUserService userService, IValidator<RegisterUserDto> validator, RegisterUserDto registerDto) =>
        {
            var validationResult = await validator.ValidateAsync(registerDto);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var result = await userService.RegisterUserAsync(registerDto);

            if (!result.IsSuccess)
            {
                return Results.BadRequest(new { Message = result.Error });
            }

            var user = result.Value!;
            var userProfile = new UserProfileDto(user.Id, user.Username, user.Role, user.Bio, user.AvatarUrl, user.JoinedAt);
            return Results.Created($"/api/users/{user.Id}", userProfile);
        })
        .WithName("RegisterUser")
        .Produces<UserProfileDto>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest);

        // POST /api/auth/login
        authGroup.MapPost("/login", async (IUserService userService, ITokenService tokenService, IValidator<LoginUserDto> validator, LoginUserDto loginDto) =>
        {
            var validationResult = await validator.ValidateAsync(loginDto);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var user = await userService.GetUserByEmailAsync(loginDto.Email);
            if (user is null || !await userService.CheckPasswordAsync(user, loginDto.Password))
            {
                return Results.Unauthorized();
            }

            var accessToken = tokenService.CreateToken(user);
            var refreshToken = tokenService.GenerateRefreshToken();
            
            await userService.SetRefreshTokenAsync(user, refreshToken, DateTime.UtcNow.AddDays(7));

            return Results.Ok(new AuthResponseDto(accessToken, refreshToken));
        })
        .WithName("LoginUser")
        .Produces<AuthResponseDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized);

        // GET /api/users/me (Example of a protected endpoint)
        userGroup.MapGet("/me", [Authorize] async (IUserService userService, HttpContext httpContext) =>
        {
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId is null)
            {
                return Results.Unauthorized();
            }
            
            var user = await userService.GetUserByIdAsync(Guid.Parse(userId));
            if (user is null)
            {
                return Results.NotFound();
            }

            var userProfile = new UserProfileDto(user.Id, user.Username, user.Role, user.Bio, user.AvatarUrl, user.JoinedAt);
            return Results.Ok(userProfile);
        })
        .WithName("GetMyProfile")
        .Produces<UserProfileDto>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .Produces(StatusCodes.Status404NotFound);
    }
}