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


    }


}
