using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManagement.Application.DTOs.Payment;
using RentalManagement.Application.Services.Interfaces;

namespace RentalManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService) => _paymentService = paymentService;

    [HttpGet("contract/{contractId}")]
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

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreatePaymentRequest request)
    {
        var result = await _paymentService.CreateAsync(request);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
    }

    [HttpPut("{id}/pay")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> MarkAsPaid(int id, [FromBody] MarkPaidRequest request)
    {
        var result = await _paymentService.MarkAsPaidAsync(id, request);
        return result.IsSuccess ? Ok(result.Data) : NotFound(result.Error);
    }
}
