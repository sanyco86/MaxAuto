namespace MaxAuto.WebApi.Services;

/// <summary>
/// Service interface to get information about the current user.
/// </summary>
public interface ICurrentUserService
{
    /// <summary>
    /// Gets the ID of the current user.
    /// </summary>
    public string? GetUserId();
}
