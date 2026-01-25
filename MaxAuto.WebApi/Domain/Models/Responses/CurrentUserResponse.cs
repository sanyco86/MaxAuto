namespace MaxAuto.WebApi.Domain.Models.Responses;

/// <summary>
/// Represents the response containing current user information.
/// </summary>
public class CurrentUserResponse
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
    /// Gets or sets the access token for the user.
    /// </summary>
    public string AccessToken { get; set; }
    
    /// <summary>
    /// Gets or sets the creation timestamp of the user.
    /// </summary>
    public DateTime CreateAt { get; set; }
    
    /// <summary>
    /// Gets or sets the update timestamp of the user.
    /// </summary>
    public DateTime UpdateAt { get; set; }
}
