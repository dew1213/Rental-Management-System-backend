using RentalManagement.Application.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalManagement.Application.DTOs.Dashboard
{
    public class DashboardDto
    {
        public int TotalHouses { get; set; }

        public int OccupiedHouses { get; set; }

        public int MaintenanceHouses { get; set; }

        public int TotalTenants { get; set; }

        public decimal MonthlyRevenue { get; set; }

        public List<PaymentDto> OverduePayments { get; set; } = [];
    }
}
