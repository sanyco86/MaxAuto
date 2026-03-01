using MaxAuto.WebApi.Infrastructure.Entities;

namespace MaxAuto.WebApi.Domain.Entities;

/// <summary>
/// Workshop.
/// </summary>
public class Workshop : EntityBase
{
    /// <summary>
    /// Name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Location.
    /// </summary>
    public required string Location { get; set; }

    /// <summary>
    /// Address.
    /// </summary>
    public string? Address { get; set; }
}
