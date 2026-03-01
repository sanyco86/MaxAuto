using AutoMapper;
using MaxAuto.WebApi.Domain.Entities;
using MaxAuto.WebApi.Domain.Models.Requests;
using MaxAuto.WebApi.Domain.Models.Responses;
using MaxAuto.WebApi.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MaxAuto.WebApi.Controllers;

/// <summary>
/// Units controller.
/// </summary>
[ApiController]
[Route("api/units")]
public sealed class UnitsController(IUnitRepository unitRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<UnitResponse>>> GetAll(CancellationToken ct)
    {
        var items = await unitRepository.GetAllAsync(ct);
        return Ok(mapper.Map<IReadOnlyList<UnitResponse>>(items));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<UnitResponse>> GetById(Guid id, CancellationToken ct)
    {
        var entity = await unitRepository.GetByIdAsync(id, ct);
        if (entity is null) return NotFound();

        return Ok(mapper.Map<UnitResponse>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<UnitResponse>> Create([FromBody] UnitRequest request, CancellationToken ct)
    {
        var entity = mapper.Map<Unit>(request);
        entity = await unitRepository.AddAsync(entity, ct);

        var dto = mapper.Map<UnitResponse>(entity);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<UnitResponse>> Update(
        Guid id,
        [FromBody] UnitRequest request,
        CancellationToken ct)
    {
        var existing = await unitRepository.GetByIdAsync(id, ct);
        if (existing is null)
            return NotFound();

        existing.Name = request.Name;

        var updated = await unitRepository.UpdateAsync(existing, ct);

        return Ok(mapper.Map<UnitResponse>(updated));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var ok = await unitRepository.DeleteAsync(id, ct);
        return ok ? NoContent() : NotFound();
    }
}
