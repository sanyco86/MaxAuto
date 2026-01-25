namespace MaxAuto.WebApi.Domain.Models.Requests;

/// <summary>
/// Represents a request to update user information.
/// </summary>
public class UpdateUserRequest
{
    /// <summary>
    /// Gets or sets the first name of the user.
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// Gets or sets the last name of the user.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Gets or sets the email of the user.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Gets or sets the password of the user.
    /// </summary>
    public string Password { get; set; }
}
