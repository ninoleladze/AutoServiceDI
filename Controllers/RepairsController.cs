using AutoService_DI.DTOs;
using AutoService_DI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoService_DI.Controllers;

[ApiController]
[Route("repairs")]
public class RepairsController : ControllerBase
{
    private readonly IRepairService _service;

    public RepairsController(IRepairService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<RepairListItemDto>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<RepairDetailDto>> GetById(int id)
    {
        var repair = await _service.GetByIdAsync(id);
        if (repair == null) return NotFound();
        return Ok(repair);
    }

    [HttpPost]
    public async Task<ActionResult<RepairDetailDto>> Create(RepairDto dto)
    {
        var error = await CheckReferencesAsync(dto);
        if (error != null) return error;

        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, RepairDto dto)
    {
        if (!await _service.ExistsAsync(id)) return NotFound();

        var error = await CheckReferencesAsync(dto);
        if (error != null) return error;

        return await _service.UpdateAsync(id, dto) ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => await _service.DeleteAsync(id) ? NoContent() : NotFound();

    private async Task<ActionResult?> CheckReferencesAsync(RepairDto dto)
    {
        if (!await _service.CarExistsAsync(dto.CarId))
            ModelState.AddModelError(nameof(dto.CarId), $"მანქანა Id={dto.CarId} ვერ მოიძებნა");

        var missing = await _service.GetMissingMechanicIdsAsync(dto.MechanicIds);
        if (missing.Count > 0)
            ModelState.AddModelError(nameof(dto.MechanicIds), $"მექანიკოსები ვერ მოიძებნა: {string.Join(", ", missing)}");

        return ModelState.IsValid ? null : ValidationProblem(ModelState);
    }
}
