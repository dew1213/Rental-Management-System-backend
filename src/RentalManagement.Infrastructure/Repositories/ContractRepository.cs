using Microsoft.EntityFrameworkCore;
using RentalManagement.Domain.Entities;
using RentalManagement.Domain.Enums;
using RentalManagement.Domain.Interfaces;
using RentalManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalManagement.Infrastructure.Repositories
{
    public class ContractRepository
    : GenericRepository<Contract>, IContractRepository
    {
        public ContractRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<IEnumerable<Contract>> GetAllWithDetailsAsync()
        {
            return await _context.Contracts
                .Include(c => c.House)
                .Include(c => c.Tenant)
                .ToListAsync();
        }

        public async Task<Contract?> GetByIdWithDetailsAsync(int id)
        {
            return await _context.Contracts
                .Include(c => c.House)
                .Include(c => c.Tenant)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Contract?> GetByTenantIdWithDetailsAsync(int id) 
        {
            return await _context.Contracts
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<Contract>> GetActiveContractsAsync()
        {
            return await _context.Contracts
                .Include(c => c.Tenant)
                .Include(c => c.House)
                .Where(c => c.Status == ContractStatus.Active)
                .ToListAsync();
        }
    }
}
