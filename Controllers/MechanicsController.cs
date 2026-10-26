using AutoService_DI.DTOs;
using AutoService_DI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoService_DI.Controllers;

[ApiController]
[Route("mechanics")]
public class MechanicsController : ControllerBase
{
    private readonly IMechanicService _service;

    public MechanicsController(IMechanicService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<MechanicListItemDto>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<MechanicDetailDto>> GetById(int id)
    {
        var mechanic = await _service.GetByIdAsync(id);
        if (mechanic == null) return NotFound();
        return Ok(mechanic);
    }

    [HttpPost]
    public async Task<ActionResult<MechanicDetailDto>> Create(MechanicDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, MechanicDto dto)
        => await _service.UpdateAsync(id, dto) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => await _service.DeleteAsync(id) ? NoContent() : NotFound();
}
