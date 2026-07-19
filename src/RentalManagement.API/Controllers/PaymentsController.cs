using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManagement.Application.DTOs.Payment;
using RentalManagement.Application.Services.Interfaces;
using System.Security.Claims;

namespace RentalManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _paymentService.GetAllAsync();
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _paymentService.GetByIdAsync(id);
        return result.IsSuccess ? Ok(result.Data) : NotFound(result.Error);
    }

    [HttpGet("contract/{contractId}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetByContract(int contractId)
    {
        var result = await _paymentService.GetByContractAsync(contractId);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
    }

    [HttpGet("overdue")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetOverdue()
    {
        var result = await _paymentService.GetOverdueAsync();
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
    }

    [HttpPut("{id}/pay")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Pay(int id, MarkPaidRequest request)
    {
        var result = await _paymentService.MarkAsPaidAsync(id, request);

        return result.IsSuccess
            ? Ok(result.Data)
            : NotFound(result.Error);
    }
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(CreatePaymentRequest request)
    {
        var result = await _paymentService.CreateAsync(request);

        return result.IsSuccess
            ? Ok(result.Data)
            : BadRequest(result.Error);
    }
    [HttpGet("my")]
    [Authorize(Roles = "Tenant")]
    public async Task<IActionResult> GetMy()
    {
        var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!int.TryParse(userIdClaim, out var userId))
            return Unauthorized();

        var result = await _paymentService.GetMyAsync(userId);

        return result.IsSuccess
            ? Ok(result.Data)
            : BadRequest(result.Error);
    }

}
