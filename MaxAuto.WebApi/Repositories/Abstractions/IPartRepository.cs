using MaxAuto.WebApi.Domain.Entities;

namespace MaxAuto.WebApi.Repositories.Abstractions;

public interface IPartRepository
{
    Task<Part?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyList<Part>> GetAllAsync(CancellationToken ct = default);

    Task<Part> AddAsync(Part part, CancellationToken ct = default);
    Task<Part> UpdateAsync(Part part, CancellationToken ct = default);

    Task<bool> DeleteAsync(Guid id, CancellationToken ct = default);
    Task<bool> ExistsAsync(Guid id, CancellationToken ct = default);
}
