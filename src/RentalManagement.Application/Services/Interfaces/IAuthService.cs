using RentalManagement.Application.Common;
using RentalManagement.Application.DTOs.Auth;

namespace RentalManagement.Application.Services.Interfaces;

public interface IAuthService
{
    Task<Result<LoginResponse>> AdminLoginAsync(LoginRequest request);
    Task<Result<LoginResponse>> TenantLoginAsync(LoginRequest request);
}
