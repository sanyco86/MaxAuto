namespace MaxAuto.WebApi.Infrastructure.Entities;

/// <summary>
/// EntityBase.
/// </summary>
public abstract class EntityBase
{
    /// <summary>
    /// Id.
    /// </summary>
    public Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// CreatedAt.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// UpdatedAt.
    /// </summary>
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
