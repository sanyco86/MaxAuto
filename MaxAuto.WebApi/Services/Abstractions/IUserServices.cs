using MaxAuto.WebApi.Domain.Models.Requests;
using MaxAuto.WebApi.Domain.Models.Responses;

namespace MaxAuto.WebApi.Services.Abstractions;

/// <summary>
/// Service interface for user-related operations.
/// </summary>
public interface IUserServices
{
    /// <summary>
    /// Gets all users.
    /// </summary>
    /// <returns>A list of <see cref="UserResponse"/>.</returns>
    Task<List<UserResponse>> GetAllAsync(CancellationToken ct = default);

    /// <summary>
    /// Gets the current user's information.
    /// </summary>
    /// <returns><see cref="CurrentUserResponse"/>.</returns>
    Task<CurrentUserResponse> GetCurrentUserAsync();
    
    /// <summary>
    /// Gets a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns><see cref="AuthUserResponse"/>.</returns>
    Task<AuthUserResponse> GetByIdAsync(Guid id);
    
    /// <summary>
    /// Updates a user's information.
    /// </summary>
    /// <param name="id">The ID of the user to update.</param>
    /// <param name="request">The user update request.</param>
    /// <returns><see cref="AuthUserResponse"/>.</returns>
    Task<AuthUserResponse> UpdateAsync(Guid id, UpdateUserRequest request);
    
    /// <summary>
    /// Deletes a user by their ID.
    /// </summary>
    /// <param name="id">The ID of the user to delete.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task DeleteAsync(Guid id);
    
    /// <summary>
    /// Revokes a refresh token.
    /// </summary>
    /// <param name="refreshTokenRemoveRequest">The refresh token request.</param>
    /// <returns><see cref="RevokeRefreshTokenResponse"/>.</returns>
    Task<RevokeRefreshTokenResponse> RevokeRefreshToken(RefreshTokenRequest refreshTokenRemoveRequest);
    
    /// <summary>
    /// Refreshes a JWT token using a refresh token.
    /// </summary>
    /// <param name="request">The refresh token request.</param>
    /// <returns><see cref="CurrentUserResponse"/>.</returns>
    Task<CurrentUserResponse> RefreshTokenAsync(RefreshTokenRequest request);
    
    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="request">The user login request.</param>
    /// <returns><see cref="AuthUserResponse"/>.</returns>
    Task<AuthUserResponse> LoginAsync(UserLoginRequest request);
}
