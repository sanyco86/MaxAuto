using MaxAuto.WebApi.Domain.Entities;
using MaxAuto.WebApi.Infrastructure.Context;
using MaxAuto.WebApi.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace MaxAuto.WebApi.Repositories;

public sealed class MechanicRepository(ApplicationDbContext context) : IMechanicRepository
{
    public Task<Mechanic?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => context.Mechanics.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<Mechanic>> GetAllAsync(CancellationToken ct = default)
        => await context.Mechanics.AsNoTracking().OrderBy(x => x.Name).ToListAsync(ct);

    public async Task<Mechanic> AddAsync(Mechanic mechanic, CancellationToken ct = default)
    {
        context.Mechanics.Add(mechanic);
        await context.SaveChangesAsync(ct);
        return mechanic;
    }

    public async Task<Mechanic> UpdateAsync(Mechanic mechanic, CancellationToken ct = default)
    {
        context.Mechanics.Update(mechanic);
        await context.SaveChangesAsync(ct);
        return mechanic;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await context.Mechanics.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return false;

        context.Mechanics.Remove(entity);
        await context.SaveChangesAsync(ct);
        return true;
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        => context.Mechanics.AnyAsync(x => x.Id == id, ct);
}
