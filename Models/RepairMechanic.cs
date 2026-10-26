namespace AutoService_DI.Models;

public class RepairMechanic
{
    public int RepairId { get; set; }
    public Repair Repair { get; set; } = null!;

    public int MechanicId { get; set; }
    public Mechanic Mechanic { get; set; } = null!;
}
