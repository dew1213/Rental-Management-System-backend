using Microsoft.AspNetCore.Mvc;
using RentalManagement.Application.DTOs.Auth;
using RentalManagement.Application.Services.Interfaces;

namespace RentalManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService) => _authService = authService;

    /// <summary>Admin login</summary>
    [HttpPost("admin/login")]
    public async Task<IActionResult> AdminLogin([FromBody] LoginRequest request)
    {
        var result = await _authService.AdminLoginAsync(request);
        return result.IsSuccess ? Ok(result.Data) : Unauthorized(new { message = result.Error });
    }

    /// <summary>Tenant login</summary>
    [HttpPost("tenant/login")]
    public async Task<IActionResult> TenantLogin([FromBody] LoginRequest request)
    {
        var result = await _authService.TenantLoginAsync(request);
        return result.IsSuccess ? Ok(result.Data) : Unauthorized(new { message = result.Error });
    }
}
