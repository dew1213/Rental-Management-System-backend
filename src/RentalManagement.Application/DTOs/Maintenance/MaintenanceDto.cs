using RentalManagement.Domain.Enums;

namespace RentalManagement.Application.DTOs.Maintenance;

public record MaintenanceDto(int Id, int ContractId, string Title, string Description, MaintenanceStatus Status, DateTime CreatedAt);
public record CreateMaintenanceRequest(int ContractId, string Title, string Description);

public record UpdateMaintenanceStatusRequest(MaintenanceStatus Status);

