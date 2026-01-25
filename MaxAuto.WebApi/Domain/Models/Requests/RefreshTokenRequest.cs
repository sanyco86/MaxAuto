namespace MaxAuto.WebApi.Domain.Models.Requests;

/// <summary>
/// Represents a request to refresh an authentication token.
/// </summary>
public class RefreshTokenRequest
{
    /// <summary>
    /// Gets or sets the refresh token.
    /// </summary>
    public required string RefreshToken { get; set; }
}
