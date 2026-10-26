namespace AutoService_DI.Models;

public class Repair
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal TotalCost { get; set; }
    public RepairStatus Status { get; set; } = RepairStatus.Pending;

    public int CarId { get; set; }
    public Car Car { get; set; } = null!;

    public List<RepairMechanic> RepairMechanics { get; set; } = new();
}
