using AutoMapper;
using MaxAuto.WebApi.Domain.Entities;
using MaxAuto.WebApi.Domain.Models.Requests;
using MaxAuto.WebApi.Domain.Models.Responses;
using MaxAuto.WebApi.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace MaxAuto.WebApi.Controllers;

/// <summary>
/// Parts controller.
/// </summary>
[ApiController]
[Route("api/parts")]
public sealed class PartsController(IPartRepository partRepository, IMapper mapper) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<PartResponse>>> GetAll(CancellationToken ct)
    {
        var items = await partRepository.GetAllAsync(ct);
        return Ok(mapper.Map<IReadOnlyList<PartResponse>>(items));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PartResponse>> GetById(Guid id, CancellationToken ct)
    {
        var entity = await partRepository.GetByIdAsync(id, ct);
        if (entity is null) return NotFound();

        return Ok(mapper.Map<PartResponse>(entity));
    }

    [HttpPost]
    public async Task<ActionResult<PartResponse>> Create([FromBody] PartRequest request, CancellationToken ct)
    {
        var entity = mapper.Map<Part>(request);
        entity = await partRepository.AddAsync(entity, ct);

        var dto = mapper.Map<PartResponse>(entity);
        return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult<PartResponse>> Update(
        Guid id,
        [FromBody] PartRequest request,
        CancellationToken ct)
    {
        var existing = await partRepository.GetByIdAsync(id, ct);
        if (existing is null)
            return NotFound();

        existing.Name = request.Name;

        var updated = await partRepository.UpdateAsync(existing, ct);

        return Ok(mapper.Map<PartResponse>(updated));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
    {
        var ok = await partRepository.DeleteAsync(id, ct);
        return ok ? NoContent() : NotFound();
    }
}
