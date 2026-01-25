using AutoMapper;
using MaxAuto.WebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using MaxAuto.WebApi.Domain.Models.Requests;
using MaxAuto.WebApi.Domain.Models.Responses;

namespace MaxAuto.WebApi.Services;

/// <summary>
/// Implementation of the IUserServices interface for managing user-related operations.
/// </summary>
/// <param name="tokenService">The token service for generating tokens.</param>
/// <param name="currentUserService">The current user service for retrieving current user information.</param>
/// <param name="userManager">The user manager for managing user information.</param>
/// <param name="mapper">The mapper for mapping objects.</param>
/// <param name="logger">The logger for logging information.</param>
public class UserService(ITokenService tokenService, ICurrentUserService currentUserService, UserManager<User> userManager, IMapper mapper, ILogger<UserService> logger) : IUserServices
{
    /// <inheritdoc/>
    /// <exception cref="Exception">Thrown when the email already exists or user creation fails.</exception>
    public async Task<UserResponse> RegisterAsync(UserRegisterRequest request)
    {
        logger.LogInformation("Registering user");
        var existingUser = await userManager.FindByEmailAsync(request.Email);
        if (existingUser != null)
        {
            logger.LogError("Email already exists");
            throw new Exception("Email already exists");
        }

        var newUser = mapper.Map<User>(request);

        // Generate a unique username
        newUser.UserName = GenerateUserName(request.FirstName, request.LastName);
        newUser.CreateAt = DateTime.UtcNow;
        newUser.UpdateAt = DateTime.UtcNow;
        var result = await userManager.CreateAsync(newUser, request.Password);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            logger.LogError("Failed to create user: {Errors}", errors);
            throw new Exception($"Failed to create user: {errors}");
        }
        logger.LogInformation("User created successfully");
        await tokenService.GenerateToken(newUser);
        return mapper.Map<UserResponse>(newUser);
    }

    /// <inheritdoc/>
    /// <exception cref="ArgumentNullException">Thrown when the login request is null.</exception>
    /// <exception cref="Exception">Thrown when the email or password is invalid or user update fails.</exception>
    public async Task<UserResponse> LoginAsync(UserLoginRequest? request)
    {
        if (request == null)
        {
            logger.LogError("Login request is null");
            throw new ArgumentNullException(nameof(request));
        }

        var user = await userManager.FindByEmailAsync(request.Email);
        if (user == null || !await userManager.CheckPasswordAsync(user, request.Password))
        {
            logger.LogError("Invalid email or password");
            throw new Exception("Invalid email or password");
        }

        // Generate access token
        var token = await tokenService.GenerateToken(user);

        // Generate refresh token
        var refreshToken = tokenService.GenerateRefreshToken();

        // Hash the refresh token and store it in the database or override the existing refresh token
        using var sha256 = SHA256.Create();
        var refreshTokenHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(refreshToken));
        user.RefreshToken = Convert.ToBase64String(refreshTokenHash);
        user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(2);

        user.CreateAt = DateTime.UtcNow;

        // Update user information in database
        var result = await userManager.UpdateAsync(user);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            logger.LogError("Failed to update user: {Errors}", errors);
            throw new Exception($"Failed to update user: {errors}");
        }

        var userResponse = mapper.Map<User, UserResponse>(user);
        userResponse.AccessToken = token;
        userResponse.RefreshToken = refreshToken;

        return userResponse;
    }

    /// <inheritdoc/>
    /// <exception cref="Exception">Thrown when the user is not found.</exception>
    public async Task<UserResponse> GetByIdAsync(Guid id)
    {
        logger.LogInformation("Getting user by id");
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            logger.LogError("User not found");
            throw new Exception("User not found");
        }
        logger.LogInformation("User found");
        return mapper.Map<UserResponse>(user);
    }

    /// <inheritdoc/>
    /// <exception cref="Exception">Thrown when the user is not found.</exception>
    public async Task<CurrentUserResponse> GetCurrentUserAsync()
    {
        var userId = currentUserService.GetUserId();
        var user = await userManager.FindByIdAsync(userId);
        if (user == null)
        {
            logger.LogError("User not found");
            throw new Exception("User not found");
        }
        return mapper.Map<CurrentUserResponse>(user);
    }

    /// <inheritdoc/>
    /// <exception cref="Exception">Thrown when the refresh token is invalid or expired.</exception>
    public async Task<CurrentUserResponse> RefreshTokenAsync(RefreshTokenRequest request)
    {
        logger.LogInformation("RefreshToken");

        // Hash the incoming RefreshToken and compare it with the one stored in the database
        using var sha256 = SHA256.Create();
        var refreshTokenHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(request.RefreshToken));
        var hashedRefreshToken = Convert.ToBase64String(refreshTokenHash);

        // Find user based on the refresh token
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == hashedRefreshToken);
        if (user == null)
        {
            logger.LogError("Invalid refresh token");
            throw new Exception("Invalid refresh token");
        }

        // Validate the refresh token expiry time
        if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
        {
            logger.LogWarning("Refresh token expired for user ID: {UserId}", user.Id);
            throw new Exception("Refresh token expired");
        }

        // Generate a new access token
        var newAccessToken = await tokenService.GenerateToken(user);
        logger.LogInformation("Access token generated successfully");
        var currentUserResponse = mapper.Map<CurrentUserResponse>(user);
        currentUserResponse.AccessToken = newAccessToken;
        return currentUserResponse;
    }

    /// <inheritdoc/>
    /// <exception cref="Exception">Thrown when the refresh token is invalid or expired.</exception>
    public async Task<RevokeRefreshTokenResponse> RevokeRefreshToken(RefreshTokenRequest refreshTokenRemoveRequest)
    {
        logger.LogInformation("Revoking refresh token");

        try
        {
            // Hash the refresh token
            using var sha256 = SHA256.Create();
            var refreshTokenHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(refreshTokenRemoveRequest.RefreshToken));
            var hashedRefreshToken = Convert.ToBase64String(refreshTokenHash);

            // Find the user based on the refresh token
            var user = await userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == hashedRefreshToken);
            if (user == null)
            {
                logger.LogError("Invalid refresh token");
                throw new Exception("Invalid refresh token");
            }

            // Validate the refresh token expiry time
            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                logger.LogWarning("Refresh token expired for user ID: {UserId}", user.Id);
                throw new Exception("Refresh token expired");
            }

            // Remove the refresh token
            user.RefreshToken = null;
            user.RefreshTokenExpiryTime = null;

            // Update user information in database
            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                logger.LogError("Failed to update user");
                return new RevokeRefreshTokenResponse
                {
                    Message = "Failed to revoke refresh token"
                };
            }
            logger.LogInformation("Refresh token revoked successfully");
            return new RevokeRefreshTokenResponse
            {
                Message = "Refresh token revoked successfully"
            };
        }
        catch (Exception ex)
        {
            logger.LogError("Failed to revoke refresh token: {Message}", ex.Message);
            throw new Exception("Failed to revoke refresh token");
        }
    }

    /// <inheritdoc/>
    /// <exception cref="Exception">Thrown when the user is not found.</exception>
    public async Task<UserResponse> UpdateAsync(Guid id, UpdateUserRequest request)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            logger.LogError("User not found");
            throw new Exception("User not found");
        }

        user.UpdateAt = DateTime.UtcNow;
        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Email = request.Email;

        await userManager.UpdateAsync(user);
        return mapper.Map<UserResponse>(user);
    }

    /// <inheritdoc/>
    /// <exception cref="Exception">Thrown when the user is not found.</exception>
    public async Task DeleteAsync(Guid id)
    {
        var user = await userManager.FindByIdAsync(id.ToString());
        if (user == null)
        {
            logger.LogError("User not found");
            throw new Exception("User not found");
        }
        await userManager.DeleteAsync(user);
    }

    private string GenerateUserName(string firstName, string lastName)
    {
        var baseUsername = $"{firstName}{lastName}".ToLower();

        // Check if the username already exists
        var username = baseUsername;
        var count = 1;
        while (userManager.Users.Any(u => u.UserName == username))
        {
            username = $"{baseUsername}{count}";
            count++;
        }
        return username;
    }
}
