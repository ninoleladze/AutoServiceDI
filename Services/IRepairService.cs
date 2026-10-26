using AutoService_DI.DTOs;

namespace AutoService_DI.Services;

public interface IRepairService
{
    Task<List<RepairListItemDto>> GetAllAsync();
    Task<RepairDetailDto?> GetByIdAsync(int id);
    Task<RepairDetailDto> CreateAsync(RepairDto dto);
    Task<bool> UpdateAsync(int id, RepairDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> CarExistsAsync(int carId);
    Task<List<int>> GetMissingMechanicIdsAsync(List<int> mechanicIds);
}
