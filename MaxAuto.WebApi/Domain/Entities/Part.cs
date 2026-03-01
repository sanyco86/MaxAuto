using MaxAuto.WebApi.Infrastructure.Entities;

namespace MaxAuto.WebApi.Domain.Entities;

/// <summary>
/// Part.
/// </summary>
public class Part : EntityBase
{
    /// <summary>
    /// Name.
    /// </summary>
    public required string Name { get; set; }
}
