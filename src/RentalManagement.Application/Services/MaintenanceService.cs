using RentalManagement.Application.Common;
using RentalManagement.Application.DTOs.Maintenance;
using RentalManagement.Application.Services.Interfaces;
using RentalManagement.Domain.Interfaces;

namespace RentalManagement.Application.Services;

public class MaintenanceService : IMaintenanceService
{
    private readonly IUnitOfWork _unitOfWork;

    public MaintenanceService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<MaintenanceDto>>> GetAllAsync()
    {
        var requests = await _unitOfWork.MaintenanceRequests.GetAllAsync();

        var result = requests.Select(r => new MaintenanceDto(
            r.Id,
            r.ContractId,
            r.Title,
            r.Description,
            r.Status,
            r.CreatedAt
        ));

        return Result<IEnumerable<MaintenanceDto>>.Success(result);
    }

    public async Task<Result<MaintenanceDto>> UpdateStatusAsync(
        int id,
        UpdateMaintenanceStatusRequest request)
    {
        var maintenance =
            await _unitOfWork.MaintenanceRequests.GetByIdAsync(id);

        if (maintenance == null)
            return Result<MaintenanceDto>.Failure("Maintenance not found.");

        maintenance.Status = request.Status;
        maintenance.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.MaintenanceRequests.Update(maintenance);
        await _unitOfWork.SaveChangesAsync();

        return Result<MaintenanceDto>.Success(
            new MaintenanceDto(
                maintenance.Id,
                maintenance.ContractId,
                maintenance.Title,
                maintenance.Description,
                maintenance.Status,
                maintenance.CreatedAt
            ));
    }
}