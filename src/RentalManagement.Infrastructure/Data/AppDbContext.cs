using Microsoft.EntityFrameworkCore;
using RentalManagement.Domain.Entities;

namespace RentalManagement.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<House> Houses => Set<House>();
    public DbSet<Tenant> Tenants => Set<Tenant>();
    public DbSet<Contract> Contracts => Set<Contract>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<MaintenanceRequest> MaintenanceRequests => Set<MaintenanceRequest>();
    public DbSet<Admin> Admins => Set<Admin>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<House>(e => {
            e.HasKey(h => h.Id);
            e.Property(h => h.MonthlyRent).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Contract>(e => {
            e.HasOne(c => c.House).WithMany(h => h.Contracts).HasForeignKey(c => c.HouseId);
            e.HasOne(c => c.Tenant).WithMany(t => t.Contracts).HasForeignKey(c => c.TenantId);
            e.Property(c => c.MonthlyRent).HasPrecision(18, 2);
        });

        modelBuilder.Entity<Payment>(e => {
            e.HasOne(p => p.Contract).WithMany(c => c.Payments).HasForeignKey(p => p.ContractId);
            e.Property(p => p.Amount).HasPrecision(18, 2);
        });

        modelBuilder.Entity<MaintenanceRequest>(e => {
            e.HasOne(m => m.Contract).WithMany().HasForeignKey(m => m.ContractId);
        });
    }
}
