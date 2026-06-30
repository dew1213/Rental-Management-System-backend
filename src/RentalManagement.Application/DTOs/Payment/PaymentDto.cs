using RentalManagement.Domain.Enums;

namespace RentalManagement.Application.DTOs.Payment;

public record PaymentDto(int Id, int ContractId, decimal Amount, DateTime DueDate, DateTime? PaidDate, PaymentStatus Status, string? Note);
public record CreatePaymentRequest(int ContractId, decimal Amount, DateTime DueDate);
public record MarkPaidRequest(DateTime PaidDate, string? Note);
