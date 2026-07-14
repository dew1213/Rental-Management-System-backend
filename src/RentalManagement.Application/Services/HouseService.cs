
using RentalManagement.Application.Common;
using RentalManagement.Application.DTOs.House;
using RentalManagement.Application.Services.Interfaces;
using RentalManagement.Domain.Entities;
using RentalManagement.Domain.Enums;
using RentalManagement.Domain.Interfaces;

namespace RentalManagement.Application.Services;

public class HouseService : IHouseService
{
    private readonly IUnitOfWork _unitOfWork;

    public HouseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    // ================= GET ALL =================
    public async Task<Result<IEnumerable<HouseDto>>> GetAllAsync()
    {
        var houses = await _unitOfWork.Houses.GetAllAsync();

        var result = houses.Select(h => new HouseDto(
            h.Id,
            h.Name,
            h.Address,
            h.MonthlyRent,
            h.Status,
            h.ImageUrl,
            h.CreatedAt
        ));

        return Result<IEnumerable<HouseDto>>.Success(result);
    }

    // ================= GET BY ID =================
    public async Task<Result<HouseDto>> GetByIdAsync(int id)
    {
        var house = await _unitOfWork.Houses.GetByIdAsync(id);

        if (house == null)
            return Result<HouseDto>.Failure("House not found");

        return Result<HouseDto>.Success(new HouseDto(
            house.Id,
            house.Name,
            house.Address,
            house.MonthlyRent,
            house.Status,
            house.ImageUrl,
            house.CreatedAt
        ));
    }

    // ================= CREATE =================
    public async Task<Result<HouseDto>> CreateAsync(CreateHouseRequest request)
    {
        var house = new House
        {
            Name = request.Name,
            Address = request.Address,
            MonthlyRent = request.MonthlyRent,
            Status = Domain.Enums.HouseStatus.Available,
            CreatedAt = DateTime.UtcNow
        };

        await _unitOfWork.Houses.AddAsync(house);
        await _unitOfWork.SaveChangesAsync();

        return Result<HouseDto>.Success(new HouseDto(
            house.Id,
            house.Name,
            house.Address,
            house.MonthlyRent,
            house.Status,
            house.ImageUrl,
            house.CreatedAt
        ));
    }

    // ================= UPDATE =================
    public async Task<Result<HouseDto>> UpdateAsync(int id, UpdateHouseRequest request)
    {
        var house = await _unitOfWork.Houses.GetByIdAsync(id);

        if (house == null)
            return Result<HouseDto>.Failure("House not found");

        house.Name = request.Name;
        house.Address = request.Address;
        house.MonthlyRent = request.MonthlyRent;
        house.Status = request.Status;
        house.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Houses.Update(house);
        await _unitOfWork.SaveChangesAsync();

        return Result<HouseDto>.Success(new HouseDto(
            house.Id,
            house.Name,
            house.Address,
            house.MonthlyRent,
            house.Status,
            house.ImageUrl,
            house.CreatedAt
        ));
    }

    // ================= DELETE =================
    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var house = await _unitOfWork.Houses.GetByIdAsync(id);

        if (house == null)
            return Result<bool>.Failure("House not found");

        _unitOfWork.Houses.Remove(house);
        await _unitOfWork.SaveChangesAsync();

        return Result<bool>.Success(true);
    }

    // ================= UPLOAD IMAGE =================
    public async Task<Result<HouseDto>> UploadImageAsync(int id, string imageUrl)
    {
        var house = await _unitOfWork.Houses.GetByIdAsync(id);

        if (house == null)
            return Result<HouseDto>.Failure("House not found");

        house.ImageUrl = imageUrl;
        house.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Houses.Update(house);
        await _unitOfWork.SaveChangesAsync();

        return Result<HouseDto>.Success(new HouseDto(
            house.Id,
            house.Name,
            house.Address,
            house.MonthlyRent,
            house.Status,
            house.ImageUrl,
            house.CreatedAt
        ));
    }

    public async Task<Result<IEnumerable<HouseDto>>> GetAvailableAsync()
    {
        var houses = await _unitOfWork.Houses.GetAllAsync();

        var result = houses
            .Where(h => h.Status == HouseStatus.Available)
            .Select(h => new HouseDto(
                h.Id,
                h.Name,
                h.Address,
                h.MonthlyRent,
                h.Status,
                h.ImageUrl,
                h.CreatedAt
            ));

        return Result<IEnumerable<HouseDto>>.Success(result);
    }

}