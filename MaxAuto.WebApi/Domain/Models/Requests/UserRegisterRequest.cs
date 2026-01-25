namespace MaxAuto.WebApi.Domain.Models.Requests;

/// <summary>
/// Represents a request for user registration.
/// </summary>
public class UserRegisterRequest
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public required string FirstName { get; set; }
    
    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public required string LastName { get; set; }
    
    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public required string Email { get; set; }
    
    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public required string Password { get; set; }
}
