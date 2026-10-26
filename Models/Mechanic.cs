namespace AutoService_DI.Models;

public class Mechanic
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public int ExperienceYears { get; set; }

    public List<RepairMechanic> RepairMechanics { get; set; } = new();
}
