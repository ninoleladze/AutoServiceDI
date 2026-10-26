using AutoService_DI.Models;

namespace AutoService_DI.DTOs;

public class MechanicListItemDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
    public int RepairsCount { get; set; }
}

public class MechanicDetailDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }
    public List<MechanicRepairDto> Repairs { get; set; } = new();
}

public class MechanicRepairDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public RepairStatus Status { get; set; }
    public decimal TotalCost { get; set; }
    public string CarPlate { get; set; } = string.Empty;
}
