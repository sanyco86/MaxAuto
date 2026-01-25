namespace MaxAuto.WebApi.Domain.Models.Responses;

/// <summary>
/// Represents a response for revoking a refresh token.
/// </summary>
public class RevokeRefreshTokenResponse
{
    /// <summary>
    /// Gets or sets the message indicating the result of the revocation.
    /// </summary>
    public string Message { get; set; }
}
