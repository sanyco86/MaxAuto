using MaxAuto.WebApi.Domain.Entities;

namespace MaxAuto.WebApi.Repositories.Abstractions;

public interface IMechanicRepository
{
    Task<Mechanic?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Mechanic>> GetAllAsync(CancellationToken ct = default);

    Task<Mechanic> AddAsync(Mechanic mechanic, CancellationToken ct = default);
    Task<Mechanic> UpdateAsync(Mechanic mechanic, CancellationToken ct = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
}
