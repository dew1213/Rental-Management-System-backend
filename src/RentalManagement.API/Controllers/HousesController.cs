using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RentalManagement.Application.DTOs.House;
using RentalManagement.Application.Services.Interfaces;

namespace RentalManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class HousesController : ControllerBase
{
    private readonly IHouseService _houseService;

    public HousesController(IHouseService houseService) => _houseService = houseService;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await _houseService.GetAllAsync();
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _houseService.GetByIdAsync(id);
        return result.IsSuccess ? Ok(result.Data) : NotFound(result.Error);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateHouseRequest request)
    {
        var result = await _houseService.CreateAsync(request);
        return result.IsSuccess ? CreatedAtAction(nameof(GetById), new { id = result.Data!.Id }, result.Data) : BadRequest(result.Error);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdateHouseRequest request)
    {
        var result = await _houseService.UpdateAsync(id, request);
        return result.IsSuccess ? Ok(result.Data) : NotFound(result.Error);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _houseService.DeleteAsync(id);
        return result.IsSuccess ? NoContent() : NotFound(result.Error);
    }

    [HttpPost("{id}/image")]
    public async Task<IActionResult> UploadImage(int id, IFormFile file)
    {
        // TODO: implement file upload to storage (e.g. local disk or S3)
        var imageUrl = $"/uploads/houses/{id}_{file.FileName}";
        var result = await _houseService.UploadImageAsync(id, imageUrl);
        return result.IsSuccess ? Ok(result.Data) : BadRequest(result.Error);
    }
}
