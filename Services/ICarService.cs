using AutoService_DI.DTOs;

namespace AutoService_DI.Services;

public interface ICarService
{
    Task<List<CarListItemDto>> GetAllAsync();
    Task<CarDetailDto?> GetByIdAsync(int id);
    Task<CarDetailDto> CreateAsync(CarDto dto);
    Task<bool> UpdateAsync(int id, CarDto dto);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
    Task<bool> CustomerExistsAsync(int customerId);

    Task<(InspectionResponseDto Inspection, bool Created)?> UpsertInspectionAsync(int carId, TechInspectionDto dto);
    Task<bool> DeleteInspectionAsync(int carId);
}
