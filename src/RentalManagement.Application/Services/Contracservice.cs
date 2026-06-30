
using RentalManagement.Application.Common;
using RentalManagement.Application.DTOs.Contract;
using RentalManagement.Application.Services.Interfaces;
using RentalManagement.Domain.Entities;
using RentalManagement.Domain.Enums;
using RentalManagement.Domain.Interfaces;

namespace RentalManagement.Application.Services;

public class ContractService : IContractService
{
    private readonly IUnitOfWork _unitOfWork;


    public ContractService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<ContractDto>>> GetAllAsync()
    {
        var contracts = await _unitOfWork.Contracts.GetAllWithDetailsAsync();

        var result = contracts.Select(c => new ContractDto(
            c.Id,
            c.HouseId,
            c.House.Name,
            c.TenantId,
            $"{c.Tenant.FirstName} {c.Tenant.LastName}",
            c.StartDate,
            c.EndDate,
            c.MonthlyRent,
            c.Status
        ));

        return Result<IEnumerable<ContractDto>>.Success(result);
    }

    public async Task<Result<ContractDto>> GetByIdAsync(int id)
    {
        var contract = await _unitOfWork.Contracts.GetByIdWithDetailsAsync(id);

        if (contract == null)
            return Result<ContractDto>.Failure("Contract not found.");

        return Result<ContractDto>.Success(new ContractDto(
            contract.Id,
            contract.HouseId,
            contract.House.Name,
            contract.TenantId,
            $"{contract.Tenant.FirstName} {contract.Tenant.LastName}",
            contract.StartDate,
            contract.EndDate,
            contract.MonthlyRent,
            contract.Status
        ));
    }
    public async Task<Result<ContractDto>> UpdateAsync(int id, UpdateContractRequest request)
    {
        var contract = await _unitOfWork.Contracts.GetByIdAsync(id);

        if (contract == null)
            return Result<ContractDto>.Failure("Contract not found.");

        var house = await _unitOfWork.Houses.GetByIdAsync(request.HouseId);
        if (house == null)
            return Result<ContractDto>.Failure("House not found.");

        var tenant = await _unitOfWork.Tenants.GetByIdAsync(request.TenantId);
        if (tenant == null)
            return Result<ContractDto>.Failure("Tenant not found.");

        if (request.Status == ContractStatus.Terminated)
        {
            house.Status = HouseStatus.Available;
        }
        else if (request.Status == ContractStatus.Active)
        {
            house.Status = HouseStatus.Rented;
        }

        contract.HouseId = request.HouseId;
        contract.TenantId = request.TenantId;
        contract.StartDate = request.StartDate.ToUniversalTime();
        contract.EndDate = request.EndDate.ToUniversalTime();
        contract.MonthlyRent = request.MonthlyRent;
        contract.Status = request.Status;

        _unitOfWork.Contracts.Update(contract);
        await _unitOfWork.SaveChangesAsync();

        return Result<ContractDto>.Success(new ContractDto(
            contract.Id,
            contract.HouseId,
            house.Name,
            contract.TenantId,
            $"{tenant.FirstName} {tenant.LastName}",
            contract.StartDate,
            contract.EndDate,
            contract.MonthlyRent,
            contract.Status
        ));
    }

    public async Task<Result<ContractDto>> CreateAsync(CreateContractRequest request)
    {
        var house = await _unitOfWork.Houses.GetByIdAsync(request.HouseId);

        if (house == null)
            return Result<ContractDto>.Failure("House not found.");

        var tenant = await _unitOfWork.Tenants.GetByIdAsync(request.TenantId);

        if (tenant == null)
            return Result<ContractDto>.Failure("Tenant not found.");

        var contract = new Contract
        {
            HouseId = request.HouseId,
            TenantId = request.TenantId,
            StartDate = request.StartDate.ToUniversalTime(),
            EndDate = request.EndDate.ToUniversalTime(),
            MonthlyRent = request.MonthlyRent
        };

        await _unitOfWork.Contracts.AddAsync(contract);

        // บ้านเปลี่ยนสถานะเป็นเช่าแล้ว
        house.Status = Domain.Enums.HouseStatus.Rented;
        _unitOfWork.Houses.Update(house);

        await _unitOfWork.SaveChangesAsync();

        return Result<ContractDto>.Success(new ContractDto(
            contract.Id,
            contract.HouseId,
            house.Name,
            contract.TenantId,
            $"{tenant.FirstName} {tenant.LastName}",
            contract.StartDate,
            contract.EndDate,
            contract.MonthlyRent,
            contract.Status
        ));
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var contract = await _unitOfWork.Contracts.GetByIdAsync(id);

        if (contract == null)
            return Result<bool>.Failure("Contract not found.");

        _unitOfWork.Contracts.Remove(contract);
        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }
}