using MaxAuto.WebApi.Domain.Entities;
using MaxAuto.WebApi.Infrastructure.Context;
using MaxAuto.WebApi.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace MaxAuto.WebApi.Repositories;

public sealed class WorkshopRepository(ApplicationDbContext context) : IWorkshopRepository
{
    public Task<Workshop?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => context.Workshops.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<Workshop>> GetAllAsync(CancellationToken ct = default)
        => await context.Workshops.AsNoTracking().OrderBy(x => x.Name).ToListAsync(ct);

    public async Task<Workshop> AddAsync(Workshop workshop, CancellationToken ct = default)
    {
        context.Workshops.Add(workshop);
        await context.SaveChangesAsync(ct);
        return workshop;
    }

    public async Task<Workshop> UpdateAsync(Workshop workshop, CancellationToken ct = default)
    {
        context.Workshops.Update(workshop);
        await context.SaveChangesAsync(ct);
        return workshop;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await context.Workshops.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return false;

        context.Workshops.Remove(entity);
        await context.SaveChangesAsync(ct);
        return true;
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        => context.Workshops.AnyAsync(x => x.Id == id, ct);
}
