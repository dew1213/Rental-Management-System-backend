using RentalManagement.Domain.Entities;

namespace RentalManagement.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IHouseRepository Houses { get; }
    ITenantRepository Tenants { get; }
    IContractRepository Contracts { get; }
    IPaymentRepository Payments { get; }
    IMaintenanceRepository MaintenanceRequest { get; }
    IGenericRepository<Admin> Admins { get; }
    Task<int> SaveChangesAsync();
}
