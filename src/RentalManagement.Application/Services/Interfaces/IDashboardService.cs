using RentalManagement.Application.Common;
using RentalManagement.Application.DTOs.Dashboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalManagement.Application.Services.Interfaces
{
    public interface IDashboardService
    {
        Task<Result<DashboardDto>> GetDashboardAsync();
        Task<Result<TenantDashboardDto>> GetTenantDashboardAsync(int userId);
    }
}
