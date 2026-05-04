using Microsoft.EntityFrameworkCore;
using ServiceOrderApi.Models;


namespace ServiceOrderApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Equipment> Equipment => Set<Equipment>();
    public DbSet<ServiceOrder> ServiceOrders => Set<ServiceOrder>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


       
        modelBuilder.Entity<Equipment>()
                    .HasOne(c => c.Customer)
                    .WithMany(e => e.Equipment)
                    .HasForeignKey(c => c.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

         modelBuilder.Entity<Equipment>()
                    .HasIndex(e => e.SerialNumber)
                    .IsUnique();

         modelBuilder.Entity<ServiceOrder>()
                     .HasOne(c => c.Customer)
                     .WithMany(s => s.ServiceOrders)
                     .HasForeignKey(c => c.CustomerId)
                     .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<ServiceOrder>()            
                    .HasOne(e => e.Equipment)
                    .WithMany(s => s.ServiceOrders)
                    .HasForeignKey(e => e.EquipmentId)
                    .OnDelete(DeleteBehavior.Restrict);

        // -- Seed data ----------------------------------------------------
        // Static dates only — HasData requires deterministic values, so we
        // can't use DateTime.UtcNow here. Migrations would otherwise change
        // every time you regenerate.
        var seedDate = new DateTime(2025, 1, 15, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Customer>().HasData(
            new Customer
            {
                Id = 1,
                Name = "Acme Corporation",
                Phone = "555-0101",
                Email = "facilities@acme.com",
                Address = "100 Industrial Way, Springfield, IL",
                CreatedAt = seedDate
            },
            new Customer
            {
                Id = 2,
                Name = "Riverside Apartments",
                Phone = "555-0202",
                Email = "manager@riversideapts.com",
                Address = "200 River Rd, Springfield, IL",
                CreatedAt = seedDate
            },
            new Customer
            {
                Id = 3,
                Name = "Downtown Diner",
                Phone = "555-0303",
                Email = "owner@downtowndiner.com",
                Address = "50 Main St, Springfield, IL",
                CreatedAt = seedDate
            }
        );

        modelBuilder.Entity<Equipment>().HasData(
            new Equipment
            {
                Id = 1,
                CustomerId = 1,
                SerialNumber = "AC-2023-001",
                Model = "Carrier 24ABC6",
                Type = "AC",
                InstallDate = new DateTime(2023, 6, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Equipment
            {
                Id = 2,
                CustomerId = 1,
                SerialNumber = "HT-2023-002",
                Model = "Trane XR16",
                Type = "Heater",
                InstallDate = new DateTime(2023, 6, 1, 0, 0, 0, DateTimeKind.Utc)
            },
            new Equipment
            {
                Id = 3,
                CustomerId = 2,
                SerialNumber = "AC-2024-100",
                Model = "Lennox EL16XC1",
                Type = "AC",
                InstallDate = new DateTime(2024, 4, 15, 0, 0, 0, DateTimeKind.Utc)
            },
            new Equipment
            {
                Id = 4,
                CustomerId = 3,
                SerialNumber = "VT-2024-050",
                Model = "Greenheck CUE-220",
                Type = "Ventilation",
                InstallDate = new DateTime(2024, 8, 10, 0, 0, 0, DateTimeKind.Utc)
            }
        );

        modelBuilder.Entity<ServiceOrder>().HasData(
            new ServiceOrder
            {
                Id = 1,
                CustomerId = 1,
                EquipmentId = 1,
                TechnicianName = "Mike Johnson",
                Description = "Annual maintenance and filter replacement",
                Status = "Completed",
                ServiceDate = new DateTime(2025, 1, 10, 9, 0, 0, DateTimeKind.Utc),
                Cost = 250.00m,
                CreatedAt = seedDate
            },
            new ServiceOrder
            {
                Id = 2,
                CustomerId = 2,
                EquipmentId = 3,
                TechnicianName = "Sarah Chen",
                Description = "AC unit not cooling properly, refrigerant low",
                Status = "InProgress",
                ServiceDate = new DateTime(2025, 1, 14, 14, 0, 0, DateTimeKind.Utc),
                Cost = 450.00m,
                CreatedAt = seedDate
            },
            new ServiceOrder
            {
                Id = 3,
                CustomerId = 3,
                EquipmentId = 4,
                TechnicianName = "Mike Johnson",
                Description = "Kitchen exhaust fan making loud noise — inspect bearings",
                Status = "Pending",
                ServiceDate = new DateTime(2025, 1, 20, 10, 0, 0, DateTimeKind.Utc),
                Cost = 0.00m,
                CreatedAt = seedDate
            }
        );


    }


}
