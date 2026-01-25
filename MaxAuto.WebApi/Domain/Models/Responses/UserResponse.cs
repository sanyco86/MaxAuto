namespace MaxAuto.WebApi.Domain.Models.Responses;

/// <summary>
/// Represents a user response.
/// </summary>
public class UserResponse
{
    /// <summary>
    /// Gets or sets the unique identifier of the user.
    /// </summary>
    public Guid Id { get; set; }
    
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
    /// Gets or sets the creation timestamp of the user.
    /// </summary>
    public DateTime CreateAt { get; set; }
    
    /// <summary>
    /// Gets or sets the update timestamp of the user.
    /// </summary>
    public DateTime UpdateAt { get; set; }
    
    /// <summary>
    /// Gets or sets the access token for the user.
    /// </summary>
    public string? AccessToken { get; set; }
    
    /// <summary>
    /// Gets or sets the refresh token for the user.
    /// </summary>
    public string? RefreshToken { get; set; }
}
