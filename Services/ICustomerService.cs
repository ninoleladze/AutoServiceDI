using AutoService_DI.DTOs;

namespace AutoService_DI.Services;

public interface ICustomerService
{
    Task<List<CustomerListItemDto>> GetAllAsync();
    Task<CustomerDetailDto?> GetByIdAsync(int id);
    Task<List<CustomerCarWithInspectionDto>?> GetCarsAsync(int customerId);
    Task<CustomerDetailDto> CreateAsync(CustomerDto dto);
    Task<bool> UpdateAsync(int id, CustomerDto dto);
    Task<bool> DeleteAsync(int id);
}
