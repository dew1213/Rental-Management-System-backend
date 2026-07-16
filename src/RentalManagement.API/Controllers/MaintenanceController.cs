using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManagement.Application.DTOs.Maintenance;
using RentalManagement.Application.Services.Interfaces;
using RentalManagement.Domain.Enums;
using System.Security.Claims;

namespace RentalManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MaintenanceController : ControllerBase
{
    private readonly IMaintenanceService _maintenanceService;

    public MaintenanceController(IMaintenanceService maintenanceService) => _maintenanceService = maintenanceService;

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _maintenanceService.GetAllAsync();
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateMaintenanceStatusRequest status)
    {
        var result = await _maintenanceService.UpdateStatusAsync(id, status);
        return result.IsSuccess ? Ok(result.Data) : NotFound(result.Error);
    }

    [HttpGet("my")]
    [Authorize(Roles = "Tenant")]
    public async Task<IActionResult> GetMy()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var result = await _maintenanceService.GetMyAsync(userId);

        return result.IsSuccess
            ? Ok(result.Data)
            : BadRequest(result.Error);
    }

    [HttpPost]
    [Authorize(Roles = "Tenant")]
    public async Task<IActionResult> Create(
        [FromBody] CreateMaintenanceRequest request)
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var result = await _maintenanceService.CreateAsync(userId, request);

        return result.IsSuccess
            ? Ok(result.Data)
            : BadRequest(result.Error);
    }
}
