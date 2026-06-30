using RentalManagement.Domain.Common;
using RentalManagement.Domain.Enums;

namespace RentalManagement.Domain.Entities;

public class Contract : BaseEntity
{
    public int HouseId { get; set; }
    public House House { get; set; } = null!;
    public int TenantId { get; set; }
    public Tenant Tenant { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal MonthlyRent { get; set; }
    public ContractStatus Status { get; set; } = ContractStatus.Active;
    public ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
