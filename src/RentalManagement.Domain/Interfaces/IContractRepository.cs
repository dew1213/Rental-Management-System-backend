using RentalManagement.Domain.Entities;

namespace RentalManagement.Domain.Interfaces;

public interface IContractRepository : IGenericRepository<Contract>
{
    Task<IEnumerable<Contract>> GetAllWithDetailsAsync();
    Task<Contract?> GetByIdWithDetailsAsync(int id);
    Task<Contract?> GetByTenantIdWithDetailsAsync(int id);
    Task<IEnumerable<Contract>> GetActiveContractsAsync();
}