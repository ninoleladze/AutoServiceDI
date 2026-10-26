namespace AutoService_DI.DTOs;

public class CustomerListItemDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int CarsCount { get; set; }
}

public class CustomerDetailDto
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public List<CustomerCarDto> Cars { get; set; } = new();
}

public class CustomerCarDto
{
    public int Id { get; set; }
    public string Plate { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
}

public class CustomerCarWithInspectionDto
{
    public int Id { get; set; }
    public string Plate { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public InspectionShortDto? Inspection { get; set; }
}

public class InspectionShortDto
{
    public DateTime ExpiryDate { get; set; }
    public bool Passed { get; set; }
}
