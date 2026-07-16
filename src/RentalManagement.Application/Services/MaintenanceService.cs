using RentalManagement.Application.Common;
using RentalManagement.Application.DTOs.Maintenance;
using RentalManagement.Application.Services.Interfaces;
using RentalManagement.Domain.Entities;
using RentalManagement.Domain.Enums;
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
        var requests = await _unitOfWork.MaintenanceRequest.GetAllAsync();

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
            await _unitOfWork.MaintenanceRequest.GetByIdAsync(id);

        if (maintenance == null)
            return Result<MaintenanceDto>.Failure("Maintenance not found.");

        maintenance.Status = request.Status;
        maintenance.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.MaintenanceRequest.Update(maintenance);
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

    public async Task<Result<IEnumerable<MaintenanceDto>>> GetMyAsync(int userId)
    {
        var contract = await _unitOfWork.Contracts.GetByIdWithDetailsAsync(userId);

        if (contract == null)
            return Result<IEnumerable<MaintenanceDto>>
                .Failure("No active contract.");

        var requests = await _unitOfWork.MaintenanceRequest.GetByContractIdAsync(contract.Id);

        var result = requests.Select(x => new MaintenanceDto(
            x.Id,
            x.ContractId,
            x.Title,
            x.Description,
            x.Status,
            x.CreatedAt
        ));

        return Result<IEnumerable<MaintenanceDto>>
            .Success(result);

    }

    public async Task<Result<MaintenanceDto>> CreateAsync(
    int userId,
    CreateMaintenanceRequest request)
    {
        //var tenant = await _unitOfWork.Tenants.GetByUserIdAsync(userId);

        //if (tenant == null)
        //    return Result<MaintenanceDto>
        //        .Failure("Tenant not found.");

        var contract = await _unitOfWork.Contracts.GetByTenantIdWithDetailsAsync(userId);

        if (contract == null)
            return Result<MaintenanceDto>
                .Failure("No active contract.");

        var maintenance = new MaintenanceRequest
        {
            ContractId = contract.Id,
            Title = request.Title,
            Description = request.Description,
            Status = MaintenanceStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.MaintenanceRequest.AddAsync(maintenance);
        await _unitOfWork.SaveChangesAsync();

        return Result<MaintenanceDto>.Success(
            new MaintenanceDto(
                maintenance.Id,
                maintenance.ContractId,
                maintenance.Title,
                maintenance.Description,
                maintenance.Status,
                maintenance.CreatedAt
            )
        );
    }
}