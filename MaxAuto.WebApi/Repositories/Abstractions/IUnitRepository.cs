using MaxAuto.WebApi.Domain.Entities;

namespace MaxAuto.WebApi.Repositories.Abstractions;

public interface IUnitRepository
{
    Task<Unit?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Unit>> GetAllAsync(CancellationToken ct = default);

    Task<Unit> AddAsync(Unit unit, CancellationToken ct = default);
    Task<Unit> UpdateAsync(Unit unit, CancellationToken ct = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
}
