using AutoService_DI.Data;
using AutoService_DI.DTOs;
using AutoService_DI.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoService_DI.Services;

public class RepairService : IRepairService
{
    private readonly AppDbContext _db;

    public RepairService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<RepairListItemDto>> GetAllAsync()
    {
        return await _db.Repairs
            .AsNoTracking()
            .Select(r => new RepairListItemDto
            {
                Id = r.Id,
                Date = r.Date,
                Status = r.Status,
                TotalCost = r.TotalCost,
                CarPlate = r.Car.Plate,
                MechanicsCount = r.RepairMechanics.Count
            })
            .ToListAsync();
    }

    public async Task<RepairDetailDto?> GetByIdAsync(int id)
    {
        var repair = await _db.Repairs
            .AsNoTracking()
            .Include(r => r.Car)
                .ThenInclude(c => c.Customer)
            .Include(r => r.RepairMechanics)
                .ThenInclude(rm => rm.Mechanic)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (repair == null) return null;

        return new RepairDetailDto
        {
            Id = repair.Id,
            Date = repair.Date,
            Description = repair.Description,
            TotalCost = repair.TotalCost,
            Status = repair.Status,
            Car = new RepairCarDto
            {
                Id = repair.Car.Id,
                Plate = repair.Car.Plate,
                Brand = repair.Car.Brand,
                Model = repair.Car.Model,
                CustomerName = repair.Car.Customer.FullName
            },
            Mechanics = repair.RepairMechanics.Select(rm => new RepairMechanicShortDto
            {
                Id = rm.Mechanic.Id,
                FullName = rm.Mechanic.FullName
            }).ToList()
        };
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _db.Repairs.AnyAsync(r => r.Id == id);
    }

    public async Task<bool> CarExistsAsync(int carId)
    {
        return await _db.Cars.AnyAsync(c => c.Id == carId);
    }

    public async Task<List<int>> GetMissingMechanicIdsAsync(List<int> mechanicIds)
    {
        var existing = await _db.Mechanics
            .Where(m => mechanicIds.Contains(m.Id))
            .Select(m => m.Id)
            .ToListAsync();

        return mechanicIds.Except(existing).ToList();
    }

    public async Task<RepairDetailDto> CreateAsync(RepairDto dto)
    {
        var repair = new Repair
        {
            CarId = dto.CarId,
            Date = DateTime.Now,
            Description = dto.Description,
            TotalCost = dto.TotalCost,
            Status = dto.Status,
            RepairMechanics = dto.MechanicIds
                .Select(mid => new RepairMechanic { MechanicId = mid })
                .ToList()
        };

        _db.Repairs.Add(repair);
        await _db.SaveChangesAsync();

        return (await GetByIdAsync(repair.Id))!;
    }

    public async Task<bool> UpdateAsync(int id, RepairDto dto)
    {
        var repair = await _db.Repairs
            .Include(r => r.RepairMechanics)
            .FirstOrDefaultAsync(r => r.Id == id);

        if (repair == null) return false;

        repair.CarId = dto.CarId;
        repair.Description = dto.Description;
        repair.TotalCost = dto.TotalCost;
        repair.Status = dto.Status;

        var toRemove = repair.RepairMechanics
            .Where(rm => !dto.MechanicIds.Contains(rm.MechanicId))
            .ToList();
        foreach (var rm in toRemove)
            repair.RepairMechanics.Remove(rm);

        var currentIds = repair.RepairMechanics.Select(rm => rm.MechanicId).ToList();
        foreach (var mid in dto.MechanicIds.Where(mid => !currentIds.Contains(mid)))
            repair.RepairMechanics.Add(new RepairMechanic { RepairId = id, MechanicId = mid });

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var repair = await _db.Repairs.FindAsync(id);
        if (repair == null) return false;

        _db.Repairs.Remove(repair);
        await _db.SaveChangesAsync();
        return true;
    }
}
