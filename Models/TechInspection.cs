namespace AutoService_DI.Models;

public class TechInspection
{
    public int Id { get; set; }
    public DateTime IssueDate { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool Passed { get; set; }

    public int CarId { get; set; }
    public Car Car { get; set; } = null!;
}
