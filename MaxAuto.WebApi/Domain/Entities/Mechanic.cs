using MaxAuto.WebApi.Infrastructure.Entities;

namespace MaxAuto.WebApi.Domain.Entities;

/// <summary>
/// Mechanic.
/// </summary>
public class Mechanic : EntityBase
{
    /// <summary>
    /// Name.
    /// </summary>
    public required string Name { get; set; }
}
