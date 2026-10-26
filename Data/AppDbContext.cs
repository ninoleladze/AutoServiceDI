using AutoService_DI.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoService_DI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<TechInspection> TechInspections => Set<TechInspection>();
    public DbSet<Mechanic> Mechanics => Set<Mechanic>();
    public DbSet<Repair> Repairs => Set<Repair>();
    public DbSet<RepairMechanic> RepairMechanics => Set<RepairMechanic>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Customer>(e =>
        {
            e.Property(c => c.FullName).HasMaxLength(100).IsRequired();
            e.Property(c => c.Phone).HasMaxLength(9).IsRequired();
            e.Property(c => c.Email).HasMaxLength(150).IsRequired();
        });

        modelBuilder.Entity<Car>(e =>
        {
            e.Property(c => c.Plate).HasMaxLength(9).IsRequired();
            e.Property(c => c.Brand).HasMaxLength(50).IsRequired();
            e.Property(c => c.Model).HasMaxLength(50).IsRequired();

            e.HasOne(c => c.Customer)
             .WithMany(cu => cu.Cars)
             .HasForeignKey(c => c.CustomerId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<TechInspection>(e =>
        {
            e.HasOne(t => t.Car)
             .WithOne(c => c.TechInspection)
             .HasForeignKey<TechInspection>(t => t.CarId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasIndex(t => t.CarId).IsUnique();
        });

        modelBuilder.Entity<Mechanic>(e =>
        {
            e.Property(m => m.FullName).HasMaxLength(100).IsRequired();
        });

        modelBuilder.Entity<Repair>(e =>
        {
            e.Property(r => r.Description).HasMaxLength(500).IsRequired();
            e.Property(r => r.TotalCost).HasPrecision(10, 2);
            e.Property(r => r.Status).HasConversion<string>().HasMaxLength(20);

            e.HasOne(r => r.Car)
             .WithMany(c => c.Repairs)
             .HasForeignKey(r => r.CarId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<RepairMechanic>(e =>
        {
            e.HasKey(rm => new { rm.RepairId, rm.MechanicId });

            e.HasOne(rm => rm.Repair)
             .WithMany(r => r.RepairMechanics)
             .HasForeignKey(rm => rm.RepairId)
             .OnDelete(DeleteBehavior.Cascade);

            e.HasOne(rm => rm.Mechanic)
             .WithMany(m => m.RepairMechanics)
             .HasForeignKey(rm => rm.MechanicId)
             .OnDelete(DeleteBehavior.Cascade);
        });

        SeedData.Seed(modelBuilder);
    }
}
