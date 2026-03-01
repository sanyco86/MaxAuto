using MaxAuto.WebApi.Infrastructure.Entities;

namespace MaxAuto.WebApi.Domain.Entities;

/// <summary>
/// Unit.
/// </summary>
public class Unit : EntityBase
{
    /// <summary>
    /// Name.
    /// </summary>
    public required string Name { get; set; }
}
