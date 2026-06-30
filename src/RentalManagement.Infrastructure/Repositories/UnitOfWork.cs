using RentalManagement.Domain.Entities;
using RentalManagement.Domain.Interfaces;
using RentalManagement.Infrastructure.Data;

namespace RentalManagement.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public IGenericRepository<House> Houses { get; }
    public IGenericRepository<Tenant> Tenants { get; }
    public IContractRepository Contracts { get; }
    public IGenericRepository<Payment> Payments { get; }
    public IGenericRepository<MaintenanceRequest> MaintenanceRequests { get; }
    public IGenericRepository<Admin> Admins { get; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Houses = new GenericRepository<House>(context);
        Tenants = new GenericRepository<Tenant>(context);
        Contracts = new ContractRepository(context);
        Payments = new GenericRepository<Payment>(context);
        MaintenanceRequests = new GenericRepository<MaintenanceRequest>(context);
        Admins = new GenericRepository<Admin>(context);
    }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();

    public void Dispose() => _context.Dispose();
}
