using RentalManagement.Application.Common;
using RentalManagement.Application.DTOs.Contract;
using RentalManagement.Application.DTOs.Dashboard;
using RentalManagement.Application.DTOs.Payment;
using RentalManagement.Application.Services.Interfaces;
using RentalManagement.Domain.Enums;
using RentalManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentalManagement.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DashboardService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<DashboardDto>> GetDashboardAsync()
        {
            var houses = await _unitOfWork.Houses.GetAllAsync();
            var tenants = await _unitOfWork.Tenants.GetAllAsync();
            var payments = await _unitOfWork.Payments.GetAllAsync();

            var dto = new DashboardDto
            {
                TotalHouses = houses.Count(),

                OccupiedHouses = houses.Count(x => x.Status == HouseStatus.Rented),

                MaintenanceHouses = houses.Count(x => x.Status == HouseStatus.Maintenance),

                TotalTenants = tenants.Count(),

                MonthlyRevenue = payments
                    .Where(p =>
                        p.Status == PaymentStatus.Paid &&
                        p.PaidDate.HasValue &&
                        p.PaidDate.Value.Month == DateTime.UtcNow.Month &&
                        p.PaidDate.Value.Year == DateTime.UtcNow.Year)
                    .Sum(p => p.Amount),

                OverduePayments = payments
                    .Where(x => x.Status == PaymentStatus.Overdue)
                    .Select(x => new PaymentDto(
                        x.Id,
                        x.ContractId,
                        x.Amount,
                        x.DueDate,
                        x.PaidDate,
                        x.Status,
                        x.SlipImageUrl,
                        x.Note
                    ))
                    .ToList()
            };

            return Result<DashboardDto>.Success(dto);
        }

        public async Task<Result<TenantDashboardDto>> GetTenantDashboardAsync(int userId)
        {
            var tenant = await _unitOfWork.Tenants.GetByIdAsync(userId);

            if (tenant == null)
                return Result<TenantDashboardDto>.Failure("Tenant not found.");

            var contract = await _unitOfWork.Contracts
                .GetByIdWithDetailsAsync(userId);

            if (contract == null)
                return Result<TenantDashboardDto>.Success(
                    new TenantDashboardDto());

            var payments = await _unitOfWork.Payments
                .GetByContractIdAsync(contract.Id);

            var maintenances = await _unitOfWork.MaintenanceRequest
                .GetByContractIdAsync(contract.Id);

            var nextPayment = payments
                .Where(x => x.Status != PaymentStatus.Paid)
                .OrderBy(x => x.DueDate)
                .FirstOrDefault();

            return Result<TenantDashboardDto>.Success(
                new TenantDashboardDto
                {
                    Contract = new ContractDto(
                        contract.Id,
                        contract.HouseId,
                        contract.House.Name,
                        contract.TenantId,
                         $"{contract.Tenant.FirstName} {contract.Tenant.LastName}",
                        contract.StartDate,
                        contract.EndDate,
                        contract.MonthlyRent,
                        contract.Status
                    ),

                    NextPayment = nextPayment == null
                        ? null
                        : new PaymentDto(
                            nextPayment.Id,
                            nextPayment.ContractId,
                            nextPayment.Amount,
                            nextPayment.DueDate,
                            nextPayment.PaidDate,
                            nextPayment.Status,
                            nextPayment.SlipImageUrl,
                            nextPayment.Note
                        ),

                    PendingMaintenance = maintenances.Count(x =>
                        x.Status == MaintenanceStatus.Pending ||
                        x.Status == MaintenanceStatus.InProgress),

                    CompletedMaintenance = maintenances.Count(x =>
                        x.Status == MaintenanceStatus.Completed)
                });
        }
    }

}
