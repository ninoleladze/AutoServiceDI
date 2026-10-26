using AutoService_DI.DTOs;

namespace AutoService_DI.Services;

public interface IMechanicService
{
    Task<List<MechanicListItemDto>> GetAllAsync();
    Task<MechanicDetailDto?> GetByIdAsync(int id);
    Task<MechanicDetailDto> CreateAsync(MechanicDto dto);
    Task<bool> UpdateAsync(int id, MechanicDto dto);
    Task<bool> DeleteAsync(int id);
}
