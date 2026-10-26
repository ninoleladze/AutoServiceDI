using AutoService_DI.DTOs;
using AutoService_DI.Services;
using Microsoft.AspNetCore.Mvc;

namespace AutoService_DI.Controllers;

[ApiController]
[Route("customers")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;

    public CustomersController(ICustomerService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<CustomerListItemDto>>> GetAll()
        => Ok(await _service.GetAllAsync());

    [HttpGet("{id:int}")]
    public async Task<ActionResult<CustomerDetailDto>> GetById(int id)
    {
        var customer = await _service.GetByIdAsync(id);
        if (customer == null) return NotFound();
        return Ok(customer);
    }

    [HttpGet("{id:int}/cars")]
    public async Task<ActionResult<List<CustomerCarWithInspectionDto>>> GetCars(int id)
    {
        var cars = await _service.GetCarsAsync(id);
        if (cars == null) return NotFound();
        return Ok(cars);
    }

    [HttpPost]
    public async Task<ActionResult<CustomerDetailDto>> Create(CustomerDto dto)
    {
        var created = await _service.CreateAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, CustomerDto dto)
        => await _service.UpdateAsync(id, dto) ? NoContent() : NotFound();

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
        => await _service.DeleteAsync(id) ? NoContent() : NotFound();
}
