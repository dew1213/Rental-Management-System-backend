using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManagement.Application.DTOs.Contract;
using RentalManagement.Application.Services.Interfaces;

namespace RentalManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class ContractsController : ControllerBase
{
    private readonly IContractService _contractService;

    public ContractsController(IContractService contractService)
    {
        _contractService = contractService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _contractService.GetAllAsync();
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _contractService.GetByIdAsync(id);
        return result.IsSuccess ? Ok(result.Data) : NotFound(result.Error);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateContractRequest request)
    {
        var result = await _contractService.UpdateAsync(id, request);

        return result.IsSuccess
            ? Ok(result.Data)
            : NotFound(result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateContractRequest request)
    {
        var result = await _contractService.CreateAsync(request);

        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data)
            : BadRequest(result.Error);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _contractService.DeleteAsync(id);

        return result.IsSuccess
            ? NoContent()
            : NotFound(result.Error);
    }
}