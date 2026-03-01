using MaxAuto.WebApi.Infrastructure.Entities;

namespace MaxAuto.WebApi.Domain.Entities;

/// <summary>
/// Work.
/// </summary>
public class Work : EntityBase
{
    /// <summary>
    /// Name.
    /// </summary>
    public required string Name { get; set; }
}
