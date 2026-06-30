using RentalManagement.Application.Common;
using RentalManagement.Application.DTOs.House;

namespace RentalManagement.Application.Services.Interfaces;

public interface IHouseService
{
    Task<Result<IEnumerable<HouseDto>>> GetAllAsync();
    Task<Result<HouseDto>> GetByIdAsync(int id);
    Task<Result<HouseDto>> CreateAsync(CreateHouseRequest request);
    Task<Result<HouseDto>> UpdateAsync(int id, UpdateHouseRequest request);
    Task<Result<bool>> DeleteAsync(int id);
    Task<Result<HouseDto>> UploadImageAsync(int id, string imageUrl);
}
