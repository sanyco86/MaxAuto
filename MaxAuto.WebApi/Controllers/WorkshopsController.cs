using AutoMapper;
using MaxAuto.WebApi.Domain.Entities;
using MaxAuto.WebApi.Domain.Models.Requests;
using MaxAuto.WebApi.Domain.Models.Responses;
using MaxAuto.WebApi.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MaxAuto.WebApi.Controllers;

/// <summary>
/// Workshops controller.
/// </summary>
[ApiController]
[Route("api/workshops")]
public sealed class WorkshopsController(IWorkshopRepository workshopRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<WorkshopResponse>>> GetAll(CancellationToken ct)
    {
        var items = await workshopRepository.GetAllAsync(ct);
        return Ok(mapper.Map<IReadOnlyList<WorkshopResponse>>(items));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<WorkshopResponse>> GetById(Guid id, CancellationToken ct)
    {
        var entity = await workshopRepository.GetByIdAsync(id, ct);
        if (entity is null) return NotFound();

        return Ok(mapper.Map<WorkshopResponse>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<WorkshopResponse>> Create([FromBody] WorkshopRequest request, CancellationToken ct)
    {
        var entity = mapper.Map<Workshop>(request);
        entity = await workshopRepository.AddAsync(entity, ct);

        var dto = mapper.Map<WorkshopResponse>(entity);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<WorkshopResponse>> Update(
        Guid id,
        [FromBody] WorkshopRequest request,
        CancellationToken ct)
    {
        var existing = await workshopRepository.GetByIdAsync(id, ct);
        if (existing is null)
            return NotFound();

        existing.Name = request.Name;
        existing.Location = request.Location;
        existing.Address = request.Address;

        var updated = await workshopRepository.UpdateAsync(existing, ct);

        return Ok(mapper.Map<WorkshopResponse>(updated));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var ok = await workshopRepository.DeleteAsync(id, ct);
        return ok ? NoContent() : NotFound();
    }
}
