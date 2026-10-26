using AutoService_DI.DTOs;
using AutoService_DI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoService_DI.Controllers;

[ApiController]
[Route("cars")]
public class CarsController : ControllerBase
{
    private readonly ICarService _service;

    public CarsController(ICarService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<CarListItemDto>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CarDetailDto>> GetById(int id)
    {
        var car = await _service.GetByIdAsync(id);
        if (car == null) return NotFound();
        return Ok(car);
    }

    [HttpPost]
    public async Task<ActionResult<CarDetailDto>> Create(CarDto dto)
    {
        if (!await _service.CustomerExistsAsync(dto.CustomerId))
        {
            ModelState.AddModelError(nameof(dto.CustomerId), $"კლიენტი Id={dto.CustomerId} ვერ მოიძებნა");
            return ValidationProblem(ModelState);
        }

        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CarDto dto)
    {
        if (!await _service.ExistsAsync(id)) return NotFound();

        if (!await _service.CustomerExistsAsync(dto.CustomerId))
        {
            ModelState.AddModelError(nameof(dto.CustomerId), $"კლიენტი Id={dto.CustomerId} ვერ მოიძებნა");
            return ValidationProblem(ModelState);
        }

        return await _service.UpdateAsync(id, dto) ? NoContent() : NotFound();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => await _service.DeleteAsync(id) ? NoContent() : NotFound();

    [HttpPut("{id:int}/inspection")]
    public async Task<ActionResult<InspectionResponseDto>> UpsertInspection(int id, TechInspectionDto dto)
    {
        var result = await _service.UpsertInspectionAsync(id, dto);
        if (result == null) return NotFound();

        var (inspection, created) = result.Value;
        if (created)
            return CreatedAtAction(nameof(GetById), new { id }, inspection);
        return Ok(inspection);
    }

    [HttpDelete("{id:int}/inspection")]
    public async Task<IActionResult> DeleteInspection(int id)
        => await _service.DeleteInspectionAsync(id) ? NoContent() : NotFound();
}
