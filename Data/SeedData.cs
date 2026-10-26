using AutoService_DI.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoService_DI.Data;

public static class SeedData
{
    public static void Seed(ModelBuilder mb)
    {
        mb.Entity<Customer>().HasData(
            new Customer { Id = 1, FullName = "გიორგი ბერიძე", Phone = "555123456", Email = "giorgi@mail.ge" },
            new Customer { Id = 2, FullName = "ნინო კაპანაძე", Phone = "599654321", Email = "nino@mail.ge" },
            new Customer { Id = 3, FullName = "ლევან მაისურაძე", Phone = "577111222", Email = "levan@mail.ge" }
        );

        mb.Entity<Car>().HasData(
            new Car { Id = 1, Plate = "AB-123-CD", Brand = "Toyota", Model = "Prius", Year = 2015, CustomerId = 1 },
            new Car { Id = 2, Plate = "GG-777-GG", Brand = "BMW", Model = "X5", Year = 2019, CustomerId = 1 },
            new Car { Id = 3, Plate = "NN-505-KA", Brand = "Mercedes", Model = "C200", Year = 2012, CustomerId = 2 },
            new Car { Id = 4, Plate = "LM-321-TB", Brand = "Honda", Model = "Fit", Year = 2008, CustomerId = 3 }
        );

        mb.Entity<TechInspection>().HasData(
            new TechInspection { Id = 1, CarId = 1, IssueDate = new DateTime(2026, 3, 10), ExpiryDate = new DateTime(2027, 3, 10), Passed = true },
            new TechInspection { Id = 2, CarId = 3, IssueDate = new DateTime(2026, 1, 20), ExpiryDate = new DateTime(2027, 1, 20), Passed = false }
        );

        mb.Entity<Mechanic>().HasData(
            new Mechanic { Id = 1, FullName = "დავით ჯანელიძე", ExperienceYears = 12 },
            new Mechanic { Id = 2, FullName = "ირაკლი ნოზაძე", ExperienceYears = 5 },
            new Mechanic { Id = 3, FullName = "ზურაბ გელაშვილი", ExperienceYears = 20 }
        );

        mb.Entity<Repair>().HasData(
            new Repair { Id = 1, CarId = 1, Date = new DateTime(2026, 5, 2), Description = "ზეთის და ფილტრის შეცვლა", TotalCost = 120m, Status = RepairStatus.Done },
            new Repair { Id = 2, CarId = 2, Date = new DateTime(2026, 9, 15), Description = "სამუხრუჭე ხუნდების შეცვლა", TotalCost = 350m, Status = RepairStatus.InProgress },
            new Repair { Id = 3, CarId = 3, Date = new DateTime(2026, 10, 1), Description = "ძრავის დიაგნოსტიკა", TotalCost = 80m, Status = RepairStatus.Pending }
        );

        mb.Entity<RepairMechanic>().HasData(
            new RepairMechanic { RepairId = 1, MechanicId = 1 },
            new RepairMechanic { RepairId = 2, MechanicId = 1 },
            new RepairMechanic { RepairId = 2, MechanicId = 2 },
            new RepairMechanic { RepairId = 3, MechanicId = 3 }
        );
    }
}
