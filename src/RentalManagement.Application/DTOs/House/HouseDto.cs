using RentalManagement.Domain.Enums;

namespace RentalManagement.Application.DTOs.House;

public record HouseDto(int Id, string Name, string Address, decimal MonthlyRent, HouseStatus Status, string? ImageUrl, DateTime CreatedAt);
public record CreateHouseRequest(string Name, string Address, decimal MonthlyRent);
public record UpdateHouseRequest(string Name, string Address, decimal MonthlyRent, HouseStatus Status);
