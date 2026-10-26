using AutoService_DI.Data;
using AutoService_DI.DTOs;
using AutoService_DI.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoService_DI.Services;

public class CustomerService : ICustomerService
{
    private readonly AppDbContext _db;

    public CustomerService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<CustomerListItemDto>> GetAllAsync()
    {
        return await _db.Customers
            .AsNoTracking()
            .Select(c => new CustomerListItemDto
            {
                Id = c.Id,
                FullName = c.FullName,
                Phone = c.Phone,
                Email = c.Email,
                CarsCount = c.Cars.Count
            })
            .ToListAsync();
    }

    public async Task<CustomerDetailDto?> GetByIdAsync(int id)
    {
        var customer = await _db.Customers
            .AsNoTracking()
            .Include(c => c.Cars)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (customer == null) return null;

        return new CustomerDetailDto
        {
            Id = customer.Id,
            FullName = customer.FullName,
            Phone = customer.Phone,
            Email = customer.Email,
            Cars = customer.Cars.Select(car => new CustomerCarDto
            {
                Id = car.Id,
                Plate = car.Plate,
                Brand = car.Brand,
                Model = car.Model,
                Year = car.Year
            }).ToList()
        };
    }

    public async Task<List<CustomerCarWithInspectionDto>?> GetCarsAsync(int customerId)
    {
        bool exists = await _db.Customers.AnyAsync(c => c.Id == customerId);
        if (!exists) return null;

        var cars = await _db.Cars
            .AsNoTracking()
            .Include(c => c.TechInspection)
            .Where(c => c.CustomerId == customerId)
            .ToListAsync();

        return cars.Select(car => new CustomerCarWithInspectionDto
        {
            Id = car.Id,
            Plate = car.Plate,
            Brand = car.Brand,
            Model = car.Model,
            Year = car.Year,
            Inspection = car.TechInspection == null
                ? null
                : new InspectionShortDto
                {
                    ExpiryDate = car.TechInspection.ExpiryDate,
                    Passed = car.TechInspection.Passed
                }
        }).ToList();
    }

    public async Task<CustomerDetailDto> CreateAsync(CustomerDto dto)
    {
        var customer = new Customer
        {
            FullName = dto.FullName,
            Phone = dto.Phone,
            Email = dto.Email
        };

        _db.Customers.Add(customer);
        await _db.SaveChangesAsync();

        return new CustomerDetailDto
        {
            Id = customer.Id,
            FullName = customer.FullName,
            Phone = customer.Phone,
            Email = customer.Email
        };
    }

    public async Task<bool> UpdateAsync(int id, CustomerDto dto)
    {
        var customer = await _db.Customers.FindAsync(id);
        if (customer == null) return false;

        customer.FullName = dto.FullName;
        customer.Phone = dto.Phone;
        customer.Email = dto.Email;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var customer = await _db.Customers.FindAsync(id);
        if (customer == null) return false;

        _db.Customers.Remove(customer);
        await _db.SaveChangesAsync();
        return true;
    }
}
