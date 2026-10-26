using AutoService_DI.Data;
using AutoService_DI.DTOs;
using AutoService_DI.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoService_DI.Services;

public class MechanicService : IMechanicService
{
    private readonly AppDbContext _db;

    public MechanicService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<MechanicListItemDto>> GetAllAsync()
    {
        return await _db.Mechanics
            .AsNoTracking()
            .Select(m => new MechanicListItemDto
            {
                Id = m.Id,
                FullName = m.FullName,
                ExperienceYears = m.ExperienceYears,
                RepairsCount = m.RepairMechanics.Count
            })
            .ToListAsync();
    }

    public async Task<MechanicDetailDto?> GetByIdAsync(int id)
    {
        var mechanic = await _db.Mechanics
            .AsNoTracking()
            .Include(m => m.RepairMechanics)
                .ThenInclude(rm => rm.Repair)
                    .ThenInclude(r => r.Car)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (mechanic == null) return null;

        return new MechanicDetailDto
        {
            Id = mechanic.Id,
            FullName = mechanic.FullName,
            ExperienceYears = mechanic.ExperienceYears,
            Repairs = mechanic.RepairMechanics.Select(rm => new MechanicRepairDto
            {
                Id = rm.Repair.Id,
                Date = rm.Repair.Date,
                Status = rm.Repair.Status,
                TotalCost = rm.Repair.TotalCost,
                CarPlate = rm.Repair.Car.Plate
            }).ToList()
        };
    }

    public async Task<MechanicDetailDto> CreateAsync(MechanicDto dto)
    {
        var mechanic = new Mechanic
        {
            FullName = dto.FullName,
            ExperienceYears = dto.ExperienceYears
        };

        _db.Mechanics.Add(mechanic);
        await _db.SaveChangesAsync();

        return new MechanicDetailDto
        {
            Id = mechanic.Id,
            FullName = mechanic.FullName,
            ExperienceYears = mechanic.ExperienceYears
        };
    }

    public async Task<bool> UpdateAsync(int id, MechanicDto dto)
    {
        var mechanic = await _db.Mechanics.FindAsync(id);
        if (mechanic == null) return false;

        mechanic.FullName = dto.FullName;
        mechanic.ExperienceYears = dto.ExperienceYears;

        await _db.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var mechanic = await _db.Mechanics.FindAsync(id);
        if (mechanic == null) return false;

        _db.Mechanics.Remove(mechanic);
        await _db.SaveChangesAsync();
        return true;
    }
}
