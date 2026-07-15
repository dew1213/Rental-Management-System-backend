using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManagement.Application.DTOs.Payment;
using RentalManagement.Application.Services.Interfaces;

namespace RentalManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class PaymentsController : ControllerBase
{
    private readonly IPaymentService _paymentService;

    public PaymentsController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _paymentService.GetAllAsync();
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _paymentService.GetByIdAsync(id);
        return result.IsSuccess ? Ok(result.Data) : NotFound(result.Error);
    }

    [HttpGet("contract/{contractId}")]
    public async Task<IActionResult> GetByContract(int contractId)
    {
        var result = await _paymentService.GetByContractAsync(contractId);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
    }

    [HttpGet("overdue")]
    public async Task<IActionResult> GetOverdue()
    {
        var result = await _paymentService.GetOverdueAsync();
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
    }

    [HttpPut("{id}/pay")]
    public async Task<IActionResult> Pay(int id, MarkPaidRequest request)
    {
        var result = await _paymentService.MarkAsPaidAsync(id, request);

        return result.IsSuccess
            ? Ok(result.Data)
            : NotFound(result.Error);
    }
}
