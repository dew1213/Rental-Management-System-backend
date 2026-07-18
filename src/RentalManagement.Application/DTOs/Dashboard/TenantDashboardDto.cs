using RentalManagement.Application.DTOs.Contract;
using RentalManagement.Application.DTOs.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalManagement.Application.DTOs.Dashboard
{
    public class TenantDashboardDto
    {
        public ContractDto? Contract { get; set; }

        public PaymentDto? NextPayment { get; set; }

        public int PendingMaintenance { get; set; }

        public int CompletedMaintenance { get; set; }
    }
}
