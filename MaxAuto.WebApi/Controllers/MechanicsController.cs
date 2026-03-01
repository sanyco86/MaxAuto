using AutoMapper;
using MaxAuto.WebApi.Domain.Entities;
using MaxAuto.WebApi.Domain.Models.Requests;
using MaxAuto.WebApi.Domain.Models.Responses;
using MaxAuto.WebApi.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MaxAuto.WebApi.Controllers;

/// <summary>
/// Mechanics controller.
/// </summary>
[ApiController]
[Route("api/mechanics")]
public sealed class MechanicsController(IMechanicRepository mechanicRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<MechanicResponse>>> GetAll(CancellationToken ct)
    {
        var items = await mechanicRepository.GetAllAsync(ct);
        return Ok(mapper.Map<IReadOnlyList<MechanicResponse>>(items));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MechanicResponse>> GetById(Guid id, CancellationToken ct)
    {
        var entity = await mechanicRepository.GetByIdAsync(id, ct);
        if (entity is null) return NotFound();

        return Ok(mapper.Map<MechanicResponse>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<MechanicResponse>> Create([FromBody] MechanicRequest request, CancellationToken ct)
    {
        var entity = mapper.Map<Mechanic>(request);
        entity = await mechanicRepository.AddAsync(entity, ct);

        var dto = mapper.Map<MechanicResponse>(entity);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<MechanicResponse>> Update(
        Guid id,
        [FromBody] MechanicRequest request,
        CancellationToken ct)
    {
        var existing = await mechanicRepository.GetByIdAsync(id, ct);
        if (existing is null)
            return NotFound();

        existing.Name = request.Name;

        var updated = await mechanicRepository.UpdateAsync(existing, ct);

        return Ok(mapper.Map<MechanicResponse>(updated));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var ok = await mechanicRepository.DeleteAsync(id, ct);
        return ok ? NoContent() : NotFound();
    }
}
