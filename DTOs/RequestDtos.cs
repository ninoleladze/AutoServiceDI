using AutoService_DI.Models;

namespace AutoService_DI.DTOs;

public class CustomerDto
{
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

public class CarDto
{
    public string Plate { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public int CustomerId { get; set; }
}

public class TechInspectionDto
{
    public DateTime IssueDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool Passed { get; set; }
}

public class MechanicDto
{
    public string FullName { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
}

public class RepairDto
{
    public int CarId { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public RepairStatus Status { get; set; }
    public List<int> MechanicIds { get; set; } = new();
}
