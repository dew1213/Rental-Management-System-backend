using RentalManagement.Domain.Common;
using RentalManagement.Domain.Enums;

namespace RentalManagement.Domain.Entities;

public class House : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public decimal MonthlyRent { get; set; }
    public HouseStatus Status { get; set; } = HouseStatus.Available;
    public string? ImageUrl { get; set; }
    public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
}
