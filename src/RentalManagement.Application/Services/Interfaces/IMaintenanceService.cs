using RentalManagement.Application.Common;
using RentalManagement.Application.DTOs.Maintenance;
using RentalManagement.Domain.Enums;

namespace RentalManagement.Application.Services.Interfaces;

public interface IMaintenanceService
{
    Task<Result<IEnumerable<MaintenanceDto>>> GetAllAsync();
    Task<Result<MaintenanceDto>> UpdateStatusAsync(int id, UpdateMaintenanceStatusRequest status);
    Task<Result<IEnumerable<MaintenanceDto>>> GetMyAsync(int userId);
    Task<Result<MaintenanceDto>> CreateAsync(int userId, CreateMaintenanceRequest request);

}
