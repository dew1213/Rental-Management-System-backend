using RentalManagement.Domain.Common;
using RentalManagement.Domain.Enums;

namespace RentalManagement.Domain.Entities;

public class MaintenanceRequest : BaseEntity
{
    public int ContractId { get; set; }
    public Contract Contract { get; set; } = null!;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public MaintenanceStatus Status { get; set; } = MaintenanceStatus.Pending;
}
