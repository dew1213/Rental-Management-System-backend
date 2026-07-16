using RentalManagement.Domain.Entities;

namespace RentalManagement.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<House> Houses { get; }
    IGenericRepository<Tenant> Tenants { get; }
    IContractRepository Contracts { get; }
    IGenericRepository<Payment> Payments { get; }
    IMaintenanceRepository MaintenanceRequest { get; }
    IGenericRepository<Admin> Admins { get; }
    Task<int> SaveChangesAsync();
}
