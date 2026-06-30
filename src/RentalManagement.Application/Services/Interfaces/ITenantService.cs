using RentalManagement.Application.Common;
using RentalManagement.Application.DTOs.Tenant;

namespace RentalManagement.Application.Services.Interfaces;

public interface ITenantService
{
    Task<Result<IEnumerable<TenantDto>>> GetAllAsync();
    Task<Result<TenantDto>> GetByIdAsync(int id);
    Task<Result<TenantDto>> CreateAsync(CreateTenantRequest request);
    Task<Result<TenantDto>> UpdateAsync(int id, UpdateTenantRequest request);
    Task<Result<bool>> DeleteAsync(int id);
}
