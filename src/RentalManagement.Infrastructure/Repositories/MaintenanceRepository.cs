using Microsoft.EntityFrameworkCore;
using RentalManagement.Domain.Entities;
using RentalManagement.Domain.Interfaces;
using RentalManagement.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalManagement.Infrastructure.Repositories
{
    public class MaintenanceRepository
    : GenericRepository<MaintenanceRequest>,IMaintenanceRepository
    {
        public MaintenanceRepository(AppDbContext context)
        : base(context)
        {
        }

        public async Task<IEnumerable<MaintenanceRequest>> GetByContractIdAsync(int contractId)
        {
            return await _context.MaintenanceRequests
                .Where(x => x.ContractId == contractId)
                .ToListAsync();
        }

    }
}
