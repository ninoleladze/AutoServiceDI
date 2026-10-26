using AutoService_DI.Data;
using AutoService_DI.DTOs;
using AutoService_DI.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoService_DI.Services;

public class CarService : ICarService
{
    private readonly AppDbContext _db;

    public CarService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<CarListItemDto>> GetAllAsync()
    {
        return await _db.Cars
            .AsNoTracking()
            .Select(c => new CarListItemDto
            {
                Id = c.Id,
                Plate = c.Plate,
                Brand = c.Brand,
                Model = c.Model,
                Year = c.Year,
                CustomerName = c.Customer.FullName
            })
            .ToListAsync();
    }

    public async Task<CarDetailDto?> GetByIdAsync(int id)
    {
        var car = await _db.Cars
            .AsNoTracking()
            .Include(c => c.Customer)
            .Include(c => c.TechInspection)
            .Include(c => c.Repairs)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (car == null) return null;

        return MapToDetail(car);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _db.Cars.AnyAsync(c => c.Id == id);
    }

    public async Task<bool> CustomerExistsAsync(int customerId)
    {
        return await _db.Customers.AnyAsync(c => c.Id == customerId);
    }

    public async Task<CarDetailDto> CreateAsync(CarDto dto)
    {
        var car = new Car
        {
            Plate = dto.Plate,
            Brand = dto.Brand,
            Model = dto.Model,
            Year = dto.Year,
            CustomerId = dto.CustomerId
        };

        _db.Cars.Add(car);
        await _db.SaveChangesAsync();

        return (await GetByIdAsync(car.Id))!;
    }

    public async Task<bool> UpdateAsync(int id, CarDto dto)
    {
        var car = await _db.Cars.FindAsync(id);
        if (car == null) return false;

        car.Plate = dto.Plate;
        car.Brand = dto.Brand;
        car.Model = dto.Model;
        car.Year = dto.Year;
        car.CustomerId = dto.CustomerId;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var car = await _db.Cars.FindAsync(id);
        if (car == null) return false;

        _db.Cars.Remove(car);
        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<(InspectionResponseDto Inspection, bool Created)?> UpsertInspectionAsync(int carId, TechInspectionDto dto)
    {
        var car = await _db.Cars
            .Include(c => c.TechInspection)
            .FirstOrDefaultAsync(c => c.Id == carId);

        if (car == null) return null;

        bool created = false;

        if (car.TechInspection == null)
        {
            car.TechInspection = new TechInspection { CarId = carId };
            created = true;
        }

        car.TechInspection.IssueDate = dto.IssueDate;
        car.TechInspection.ExpiryDate = dto.ExpiryDate;
        car.TechInspection.Passed = dto.Passed;

        await _db.SaveChangesAsync();

        var result = new InspectionResponseDto
        {
            Id = car.TechInspection.Id,
            IssueDate = car.TechInspection.IssueDate,
            ExpiryDate = car.TechInspection.ExpiryDate,
            Passed = car.TechInspection.Passed
        };

        return (result, created);
    }

    public async Task<bool> DeleteInspectionAsync(int carId)
    {
        var inspection = await _db.TechInspections.FirstOrDefaultAsync(t => t.CarId == carId);
        if (inspection == null) return false;

        _db.TechInspections.Remove(inspection);
        await _db.SaveChangesAsync();
        return true;
    }

    private static CarDetailDto MapToDetail(Car car)
    {
        return new CarDetailDto
        {
            Id = car.Id,
            Plate = car.Plate,
            Brand = car.Brand,
            Model = car.Model,
            Year = car.Year,
            Customer = new CarCustomerDto
            {
                Id = car.Customer.Id,
                FullName = car.Customer.FullName,
                Phone = car.Customer.Phone
            },
            Inspection = car.TechInspection == null
                ? null
                : new InspectionResponseDto
                {
                    Id = car.TechInspection.Id,
                    IssueDate = car.TechInspection.IssueDate,
                    ExpiryDate = car.TechInspection.ExpiryDate,
                    Passed = car.TechInspection.Passed
                },
            Repairs = car.Repairs.Select(r => new CarRepairDto
            {
                Id = r.Id,
                Date = r.Date,
                Status = r.Status,
                TotalCost = r.TotalCost
            }).ToList()
        };
    }
}
