using MaxAuto.WebApi.Domain.Entities;

namespace MaxAuto.WebApi.Repositories.Abstractions;

public interface IWorkshopRepository
{
    Task<Workshop?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Workshop>> GetAllAsync(CancellationToken ct = default);

    Task<Workshop> AddAsync(Workshop workshop, CancellationToken ct = default);
    Task<Workshop> UpdateAsync(Workshop workshop, CancellationToken ct = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
}
