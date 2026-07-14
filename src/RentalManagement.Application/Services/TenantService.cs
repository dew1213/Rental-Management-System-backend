using BCrypt.Net;
using RentalManagement.Application.Common;
using RentalManagement.Application.DTOs.Tenant;
using RentalManagement.Application.Services.Interfaces;
using RentalManagement.Domain.Entities;
using RentalManagement.Domain.Enums;
using RentalManagement.Domain.Interfaces;

namespace RentalManagement.Application.Services;

public class TenantService : ITenantService
{
    private readonly IUnitOfWork _unitOfWork;

    public TenantService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<TenantDto>>> GetAllAsync()
    {
        var tenants = await _unitOfWork.Tenants.GetAllAsync();

        var result = tenants.Select(t => new TenantDto(
            t.Id,
            t.FirstName,
            t.LastName,
            t.Email,
            t.Phone,
            t.CreatedAt,
            t.Status
           
        ));

        return Result<IEnumerable<TenantDto>>.Success(result);
    }

    public async Task<Result<TenantDto>> GetByIdAsync(int id)
    {
        var tenant = await _unitOfWork.Tenants.GetByIdAsync(id);

        if (tenant == null)
            return Result<TenantDto>.Failure("Tenant not found.");

        var result = new TenantDto(
            tenant.Id,
            tenant.FirstName,
            tenant.LastName,
            tenant.Email,
            tenant.Phone,
            tenant.CreatedAt,
            tenant.Status
        );

        return Result<TenantDto>.Success(result);
    }

    public async Task<Result<TenantDto>> CreateAsync(CreateTenantRequest request)
    {
        var exists = (await _unitOfWork.Tenants.FindAsync(x => x.Email == request.Email)).FirstOrDefault();

        if (exists != null)
            return Result<TenantDto>.Failure("Email already exists.");

        var tenant = new Tenant
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Status = Domain.Enums.TenantsStatus.Available,
        };

        await _unitOfWork.Tenants.AddAsync(tenant);
        await _unitOfWork.SaveChangesAsync();

        return Result<TenantDto>.Success(new TenantDto(
            tenant.Id,
            tenant.FirstName,
            tenant.LastName,
            tenant.Email,
            tenant.Phone,
            tenant.CreatedAt,
            tenant.Status
        ));
    }

    public async Task<Result<TenantDto>> UpdateAsync(int id, UpdateTenantRequest request)
    {
        var tenant = await _unitOfWork.Tenants.GetByIdAsync(id);

        if (tenant == null)
            return Result<TenantDto>.Failure("Tenant not found.");

        tenant.FirstName = request.FirstName;
        tenant.LastName = request.LastName;
        tenant.Phone = request.Phone;
        tenant.Status = request.Status;

        _unitOfWork.Tenants.Update(tenant);
        await _unitOfWork.SaveChangesAsync();

        return Result<TenantDto>.Success(new TenantDto(
            tenant.Id,
            tenant.FirstName,
            tenant.LastName,
            tenant.Email,
            tenant.Phone,
            tenant.CreatedAt,
            tenant.Status
        ));
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var tenant = await _unitOfWork.Tenants.GetByIdAsync(id);

        if (tenant == null)
            return Result<bool>.Failure("Tenant not found.");

        _unitOfWork.Tenants.Remove(tenant);
        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }

    public async Task<Result<IEnumerable<TenantDto>>> GetAvailableAsync()
    {
        var tenants = await _unitOfWork.Tenants.GetAllAsync();
        var contracts = await _unitOfWork.Contracts.GetAllAsync();

        var activeTenantIds = contracts
            .Select(c => c.TenantId)
            .ToHashSet();

        var result = tenants
            .Where(t =>
                t.Status == TenantsStatus.Available &&
                !activeTenantIds.Contains(t.Id))
            .Select(t => new TenantDto(
                t.Id,
                t.FirstName,
                t.LastName,
                t.Email,
                t.Phone,
                t.CreatedAt,
                t.Status
            ));

        return Result<IEnumerable<TenantDto>>.Success(result);
    }
}