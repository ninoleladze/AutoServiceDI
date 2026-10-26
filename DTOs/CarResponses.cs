using AutoService_DI.Models;

namespace AutoService_DI.DTOs;

public class CarListItemDto
{
    public int Id { get; set; }
    public string Plate { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string CustomerName { get; set; } = string.Empty;
}

public class CarDetailDto
{
    public int Id { get; set; }
    public string Plate { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public CarCustomerDto Customer { get; set; } = null!;
    public InspectionResponseDto? Inspection { get; set; }
    public List<CarRepairDto> Repairs { get; set; } = new();
}

public class CarCustomerDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public class InspectionResponseDto
{
    public int Id { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool Passed { get; set; }
}

public class CarRepairDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public RepairStatus Status { get; set; }
    public decimal TotalCost { get; set; }
}
