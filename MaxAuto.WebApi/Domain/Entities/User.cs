using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MaxAuto.WebApi.Domain.Entities;

/// <summary>
/// User.
/// </summary>
public class User : IdentityUser
{
    /// <summary>
    /// First name.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public required string FirstName { get; set; }

    /// <summary>
    /// Last name.
    /// </summary>
    [Required]
    [MaxLength(100)]
    public required string LastName { get; set; }

    /// <summary>
    /// Role.
    /// </summary>
    [MaxLength(100)]
    public string? Role { get; set; }

    /// <summary>
    /// Position.
    /// </summary>
    [MaxLength(100)]
    public string? Position { get; set; }

    /// <summary>
    /// Workshop ID.
    /// </summary>
    public Guid? WorkshopId { get; set; }

    /// <summary>
    /// Workshop.
    /// </summary>
    public Workshop? Workshop { get; set; }

    /// <summary>
    /// Refresh token.
    /// </summary>
    [MaxLength(256)]
    public string? RefreshToken { get; set; }

    /// <summary>
    /// Refresh token expiry time.
    /// </summary>
    public DateTime? RefreshTokenExpiryTime { get; set; }

    /// <summary>
    /// Created at timestamp.
    /// </summary>
    public DateTime CreateAt { get; set; }

    /// <summary>
    /// Updated at timestamp.
    /// </summary>
    public DateTime UpdateAt { get; set; }
}
