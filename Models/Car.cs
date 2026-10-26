namespace AutoService_DI.Models;

public class Car
{
    public int Id { get; set; }
    public string Plate { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }

    public int CustomerId { get; set; }
    public Customer Customer { get; set; } = null!;

    public TechInspection? TechInspection { get; set; }

    public List<Repair> Repairs { get; set; } = new();
}
