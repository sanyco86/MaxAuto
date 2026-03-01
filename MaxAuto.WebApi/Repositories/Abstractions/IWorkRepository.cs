using MaxAuto.WebApi.Domain.Entities;

namespace MaxAuto.WebApi.Repositories.Abstractions;

public interface IWorkRepository
{
    Task<Work?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Work>> GetAllAsync(CancellationToken ct = default);

    Task<Work> AddAsync(Work work, CancellationToken ct = default);
    Task<Work> UpdateAsync(Work work, CancellationToken ct = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
}
