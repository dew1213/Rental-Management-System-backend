using RentalManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalManagement.Domain.Interfaces
{
    public interface ITenantRepository : IGenericRepository<Tenant>
    {
        Task<bool> ExistsByTenantIdAsync(int tenantId);
    }
}
