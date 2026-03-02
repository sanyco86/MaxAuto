using MaxAuto.WebApi.Domain.Models.Responses;
using MaxAuto.WebApi.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MaxAuto.WebApi.Controllers;

/// <summary>
/// Users controller.
/// </summary>
[ApiController]
[Route("api/users")]
public sealed class UsersController(IUserServices userService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UserResponse>>> GetAll(CancellationToken ct)
    {
        var items = await userService.GetAllAsync(ct);
        return Ok(items);
    }
}
