namespace MaxAuto.WebApi.Domain.Models;

/// <summary>
/// Represents the settings for JWT (JSON Web Token) authentication.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Gets or sets the secret key used for signing the JWT.
    /// </summary>
    public string? Key { get; set; }
    
    /// <summary>
    /// Gets or sets the valid issuer of the JWT.
    /// </summary>
    public string ValidIssuer { get; set; }
    
    /// <summary>
    /// Gets or sets the valid audience of the JWT.
    /// </summary>
    public string ValidAudience { get; set; }
    
    /// <summary>
    /// Gets or sets the expiration time of the JWT in minutes.
    /// </summary>
    public double Expires { get; set; }
}
