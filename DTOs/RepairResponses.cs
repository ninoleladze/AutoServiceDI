using AutoService_DI.Models;

namespace AutoService_DI.DTOs;

public class RepairListItemDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public RepairStatus Status { get; set; }
    public decimal TotalCost { get; set; }
    public string CarPlate { get; set; } = string.Empty;
    public int MechanicsCount { get; set; }
}

public class RepairDetailDto
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public RepairStatus Status { get; set; }
    public RepairCarDto Car { get; set; } = null!;
    public List<RepairMechanicShortDto> Mechanics { get; set; } = new();
}

public class RepairCarDto
{
    public int Id { get; set; }
    public string Plate { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public string CustomerName { get; set; } = string.Empty;
}

public class RepairMechanicShortDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
}
