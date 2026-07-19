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
    public class TenantRepository: GenericRepository<Tenant>,ITenantRepository
    {
        public TenantRepository(AppDbContext context)
            : base(context)
        {
        }

        public async Task<bool> ExistsByTenantIdAsync(int tenantId)
        {
            return await _context.Contracts
                .AnyAsync(c => c.TenantId == tenantId);
        }
    }
}
