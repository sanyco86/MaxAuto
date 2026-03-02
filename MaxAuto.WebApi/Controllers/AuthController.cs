using MaxAuto.WebApi.Domain.Models.Requests;
using MaxAuto.WebApi.Domain.Models.Responses;
using MaxAuto.WebApi.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaxAuto.WebApi.Controllers;

/// <summary>
/// Authentication controller.
/// </summary>
/// <param name="userService"><see cref="IUserServices"/>.</param>
[ApiController]
[Route("api/")]
public sealed class AuthController(IUserServices userService) : ControllerBase
{
    /// <summary>
    /// Logs in a user.
    /// </summary>
    /// <param name="request"><see cref="UserLoginRequest"/>.</param>
    /// <returns><see cref="AuthUserResponse"/>.</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(AuthUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AuthUserResponse>> Login([FromBody] UserLoginRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var response = await userService.LoginAsync(request);
        return Ok(response);
    }

    /// <summary>
    /// Gets a user by ID.
    /// </summary>
    /// <param name="id">The ID of the user.</param>
    /// <returns><see cref="AuthUserResponse"/>.</returns>
    [HttpGet("user/{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(AuthUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AuthUserResponse>> GetById(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var response = await userService.GetByIdAsync(id);
        return Ok(response);
    }

    /// <summary>
    /// Refreshes the access token using the refresh token.
    /// </summary>
    /// <param name="request"><see cref="RefreshTokenRequest"/>.</param>
    /// <returns><see cref="CurrentUserResponse"/>.</returns>
    [HttpPost("refresh-token")]
    [Authorize]
    [ProducesResponseType(typeof(CurrentUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CurrentUserResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var response = await userService.RefreshTokenAsync(request);
        return Ok(response);
    }

    /// <summary>
    /// Revokes the refresh token.
    /// </summary>
    /// <param name="request"><see cref="RefreshTokenRequest"/>.</param>
    /// <returns><see cref="RevokeRefreshTokenResponse"/>.</returns>
    [HttpPost("revoke-refresh-token")]
    [Authorize]
    [ProducesResponseType(typeof(RevokeRefreshTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RevokeRefreshTokenResponse>> RevokeRefreshToken([FromBody] RefreshTokenRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var response = await userService.RevokeRefreshToken(request);
        if (response.Message == "Refresh token revoked successfully")
        {
            return Ok(response);
        }
        return BadRequest(response);
    }

    /// <summary>
    /// Gets the current user.
    /// </summary>
    /// <returns><see cref="CurrentUserResponse"/>.</returns>
    [HttpGet("current-user")]
    [Authorize]
    [ProducesResponseType(typeof(CurrentUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<CurrentUserResponse>> GetCurrentUser()
    {
        var response = await userService.GetCurrentUserAsync();
        return Ok(response);
    }

    /// <summary>
    /// Update a user.
    /// </summary>
    /// <param name="id">The ID of the user to be updated.</param>
    /// <param name="request"><see cref="UpdateUserRequest"/>.</param>
    /// <returns><see cref="AuthUserResponse"/>.</returns>
    [HttpPatch("user/{id:guid}")]
    [Authorize]
    [ProducesResponseType(typeof(AuthUserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AuthUserResponse>> Update(Guid id, [FromBody] UpdateUserRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var response = await userService.UpdateAsync(id, request);
        return Ok(response);
    }

    /// <summary>
    /// Deletes a user.
    /// </summary>
    /// <param name="id">The ID of the user to be deleted.</param>
    /// <returns>An <see cref="IActionResult"/> representing the result of the operation.</returns>
    [HttpDelete("user/{id:guid}")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        await userService.DeleteAsync(id);
        return Ok();
    }
}
