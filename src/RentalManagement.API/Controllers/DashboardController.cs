using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManagement.Application.Services.Interfaces;
using System.Security.Claims;

namespace RentalManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DashboardController : ControllerBase
{
    private readonly IDashboardService _dashboardService;

    public DashboardController(IDashboardService dashboardService)
    {
        _dashboardService = dashboardService;
    }

    [HttpGet("AdminDashboard")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Get()
    {
        var result = await _dashboardService.GetDashboardAsync();

        return result.IsSuccess
            ? Ok(result.Data)
            : BadRequest(result.Error);
    }
    [HttpGet("tenantDashboard")]
    [Authorize(Roles = "Tenant")]
    public async Task<IActionResult> GetTenantDashboard()
    {
        var userId = int.Parse(
            User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var result =
            await _dashboardService.GetTenantDashboardAsync(userId);

        return result.IsSuccess
            ? Ok(result.Data)
            : BadRequest(result.Error);
    }
}
