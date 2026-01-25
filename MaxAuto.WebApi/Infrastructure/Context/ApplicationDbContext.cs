using MaxAuto.WebApi.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MaxAuto.WebApi.Infrastructure.Context;

/// <summary>
/// Represents the application's database context, integrating Identity for user management.
/// </summary>
/// <param name="options">The options for configuring the database context.</param>
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options);
