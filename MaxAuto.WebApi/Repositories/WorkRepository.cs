using MaxAuto.WebApi.Domain.Entities;
using MaxAuto.WebApi.Infrastructure.Context;
using MaxAuto.WebApi.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace MaxAuto.WebApi.Repositories;

public sealed class WorkRepository(ApplicationDbContext context) : IWorkRepository
{
    public Task<Work?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => context.Works.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<Work>> GetAllAsync(CancellationToken ct = default)
        => await context.Works.AsNoTracking().OrderBy(x => x.Name).ToListAsync(ct);

    public async Task<Work> AddAsync(Work work, CancellationToken ct = default)
    {
        context.Works.Add(work);
        await context.SaveChangesAsync(ct);
        return work;
    }

    public async Task<Work> UpdateAsync(Work work, CancellationToken ct = default)
    {
        context.Works.Update(work);
        await context.SaveChangesAsync(ct);
        return work;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await context.Works.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return false;

        context.Works.Remove(entity);
        await context.SaveChangesAsync(ct);
        return true;
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        => context.Works.AnyAsync(x => x.Id == id, ct);
}
