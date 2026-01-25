using MaxAuto.WebApi.Domain.Entities;

namespace MaxAuto.WebApi.Services;

/// <summary>
/// Service interface for generating tokens.
/// </summary>
public interface ITokenService
{
    /// <summary>
    /// Generates a JWT token for the specified user.
    /// </summary>
    /// <param name="user">The user for whom to generate the token.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the generated token.</returns>
    Task<string> GenerateToken(User user);
    
    /// <summary>
    /// Generates a refresh token.
    /// </summary>
    /// <returns>The generated refresh token.</returns>
    string GenerateRefreshToken();
}
