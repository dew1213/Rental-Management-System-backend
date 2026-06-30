using RentalManagement.Domain.Common;
using RentalManagement.Domain.Enums;

namespace RentalManagement.Domain.Entities;

public class Payment : BaseEntity
{
    public int ContractId { get; set; }
    public Contract Contract { get; set; } = null!;
    public decimal Amount { get; set; }
    public DateTime PaidDate { get; set; }
    public DateTime DueDate { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public string? Note { get; set; }
}
