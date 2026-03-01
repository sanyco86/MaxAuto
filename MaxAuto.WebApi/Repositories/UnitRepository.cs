using MaxAuto.WebApi.Domain.Entities;
using MaxAuto.WebApi.Infrastructure.Context;
using MaxAuto.WebApi.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace MaxAuto.WebApi.Repositories;

public sealed class UnitRepository(ApplicationDbContext context) : IUnitRepository
{
    public Task<Unit?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => context.Units.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<Unit>> GetAllAsync(CancellationToken ct = default)
        => await context.Units.AsNoTracking().OrderBy(x => x.Name).ToListAsync(ct);

    public async Task<Unit> AddAsync(Unit unit, CancellationToken ct = default)
    {
        context.Units.Add(unit);
        await context.SaveChangesAsync(ct);
        return unit;
    }

    public async Task<Unit> UpdateAsync(Unit unit, CancellationToken ct = default)
    {
        context.Units.Update(unit);
        await context.SaveChangesAsync(ct);
        return unit;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await context.Units.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return false;

        context.Units.Remove(entity);
        await context.SaveChangesAsync(ct);
        return true;
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        => context.Units.AnyAsync(x => x.Id == id, ct);
}
