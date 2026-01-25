using System.Security.Claims;

namespace MaxAuto.WebApi.Services;

/// <summary>
/// Service to get information about the current user.
/// </summary>
public class CurrentUserService(IHttpContextAccessor httpContextAccessor) : ICurrentUserService
{
    /// <inheritdoc/>
    public string? GetUserId()
    {
        var userId = httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        return userId;
    }
}
