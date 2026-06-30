using RentalManagement.Domain.Enums;

namespace RentalManagement.Application.DTOs.Contract;

public record ContractDto(int Id, int HouseId, string HouseName, int TenantId, string TenantName, DateTime StartDate, DateTime EndDate, decimal MonthlyRent, ContractStatus Status);
public record CreateContractRequest(int HouseId, int TenantId, DateTime StartDate, DateTime EndDate, decimal MonthlyRent);
public record UpdateContractRequest(int HouseId,int TenantId,DateTime StartDate,DateTime EndDate, decimal MonthlyRent, ContractStatus Status);
