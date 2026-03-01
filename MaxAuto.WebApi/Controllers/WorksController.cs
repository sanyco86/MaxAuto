using AutoMapper;
using MaxAuto.WebApi.Domain.Entities;
using MaxAuto.WebApi.Domain.Models.Requests;
using MaxAuto.WebApi.Domain.Models.Responses;
using MaxAuto.WebApi.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MaxAuto.WebApi.Controllers;

/// <summary>
/// Works controller.
/// </summary>
[ApiController]
[Route("api/works")]
public sealed class WorksController(IWorkRepository workRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<WorkResponse>>> GetAll(CancellationToken ct)
    {
        var items = await workRepository.GetAllAsync(ct);
        return Ok(mapper.Map<IReadOnlyList<WorkResponse>>(items));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<WorkResponse>> GetById(Guid id, CancellationToken ct)
    {
        var entity = await workRepository.GetByIdAsync(id, ct);
        if (entity is null) return NotFound();

        return Ok(mapper.Map<WorkResponse>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<WorkResponse>> Create([FromBody] WorkRequest request, CancellationToken ct)
    {
        var entity = mapper.Map<Work>(request);
        entity = await workRepository.AddAsync(entity, ct);

        var dto = mapper.Map<WorkResponse>(entity);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<WorkResponse>> Update(
        Guid id,
        [FromBody] WorkRequest request,
        CancellationToken ct)
    {
        var existing = await workRepository.GetByIdAsync(id, ct);
        if (existing is null)
            return NotFound();

        existing.Name = request.Name;

        var updated = await workRepository.UpdateAsync(existing, ct);

        return Ok(mapper.Map<WorkResponse>(updated));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var ok = await workRepository.DeleteAsync(id, ct);
        return ok ? NoContent() : NotFound();
    }
}
