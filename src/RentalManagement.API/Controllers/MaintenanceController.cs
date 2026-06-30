using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManagement.Application.DTOs.Maintenance;
using RentalManagement.Application.Services.Interfaces;
using RentalManagement.Domain.Enums;

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

    [HttpPost]
    [Authorize(Roles = "Tenant")]
    public async Task<IActionResult> Create([FromBody] CreateMaintenanceRequest request)
    {
        var result = await _maintenanceService.CreateAsync(request);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStatus(int id, [FromQuery] MaintenanceStatus status)
    {
        var result = await _maintenanceService.UpdateStatusAsync(id, status);
        return result.IsSuccess ? Ok(result.Data) : NotFound(result.Error);
    }
}
