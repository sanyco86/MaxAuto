namespace MaxAuto.WebApi.Domain.Models.Requests;

/// <summary>
/// Represents a request for user login.
/// </summary>
public class UserLoginRequest
{
    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public required string Password { get; set; }
}
