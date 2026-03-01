using MaxAuto.WebApi.Domain.Models;
using MaxAuto.WebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using MaxAuto.WebApi.Services.Abstractions;

namespace MaxAuto.WebApi.Services;

/// <summary>
/// Implementation of the ITokenService interface for generating JWT tokens and refresh tokens.
/// </summary>
/// <param name="configuration">The configuration settings.</param>
/// <param name="userManager">The user manager for managing user information.</param>
/// <exception cref="InvalidOperationException">Thrown when JWT secret key is not configured.</exception>
public class TokenService(IConfiguration configuration, UserManager<User> userManager) : ITokenService
{
    /// <inheritdoc/>
    public async Task<string> GenerateToken(User user)
    {
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.Key))
        {
            throw new InvalidOperationException("JWT secret key is not configured.");
        }
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        var singingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        var claims = await GetClaimsAsync(user);
        var tokenOptions = GenerateTokenOptions(singingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }

    /// <inheritdoc/>
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        var refreshToken = Convert.ToBase64String(randomNumber);
        return refreshToken;
    }

    private async Task<List<Claim>> GetClaimsAsync(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName ?? string.Empty),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email ?? string.Empty),
            new("FirstName", user.FirstName),
            new("LastName", user.LastName)
        };

        var roles = await userManager.GetRolesAsync(user);
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }

    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.Key))
        {
            throw new InvalidOperationException("JWT secret key is not configured.");
        }
        
        return new JwtSecurityToken(
            issuer: jwtSettings.ValidIssuer,
            audience: jwtSettings.ValidAudience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.Expires),
            signingCredentials: signingCredentials
        );
    }
}
