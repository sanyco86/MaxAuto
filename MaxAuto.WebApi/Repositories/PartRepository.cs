using MaxAuto.WebApi.Domain.Entities;
using MaxAuto.WebApi.Infrastructure.Context;
using MaxAuto.WebApi.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace MaxAuto.WebApi.Repositories;

public sealed class PartRepository(ApplicationDbContext context) : IPartRepository
{
    public Task<Part?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => context.Parts.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, ct);

    public async Task<IReadOnlyList<Part>> GetAllAsync(CancellationToken ct = default)
        => await context.Parts.AsNoTracking().OrderBy(x => x.Name).ToListAsync(ct);

    public async Task<Part> AddAsync(Part part, CancellationToken ct = default)
    {
        context.Parts.Add(part);
        await context.SaveChangesAsync(ct);
        return part;
    }

    public async Task<Part> UpdateAsync(Part part, CancellationToken ct = default)
    {
        context.Parts.Update(part);
        await context.SaveChangesAsync(ct);
        return part;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var entity = await context.Parts.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (entity is null) return false;

        context.Parts.Remove(entity);
        await context.SaveChangesAsync(ct);
        return true;
    }

    public Task<bool> ExistsAsync(Guid id, CancellationToken ct = default)
        => context.Parts.AnyAsync(x => x.Id == id, ct);
}
