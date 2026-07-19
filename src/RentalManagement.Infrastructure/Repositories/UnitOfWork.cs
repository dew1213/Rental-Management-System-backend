using RentalManagement.Domain.Entities;
using RentalManagement.Domain.Interfaces;
using RentalManagement.Infrastructure.Data;

namespace RentalManagement.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IHouseRepository Houses { get; }
    public ITenantRepository Tenants { get; }
    public IContractRepository Contracts { get; }
    public IPaymentRepository Payments { get; }
    public IMaintenanceRepository MaintenanceRequest { get; }
    public IGenericRepository<Admin> Admins { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Houses = new HouseRepository(context);
        Tenants = new TenantRepository(context);
        Contracts = new ContractRepository(context);
        Payments = new PaymentRepository(context);
        MaintenanceRequest = new MaintenanceRepository(context);
        Admins = new GenericRepository<Admin>(context);
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
