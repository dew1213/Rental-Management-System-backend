using Microsoft.AspNetCore.Http;
using RentalManagement.Domain.Enums;

namespace RentalManagement.Application.DTOs.Payment;

public record PaymentDto(int Id, int ContractId, decimal Amount, DateTime DueDate, DateTime? PaidDate, PaymentStatus Status, string? SlipImageUrl, string? Note);
public record CreatePaymentRequest(int ContractId, decimal Amount, DateTime DueDate, string? Note);
public record MarkPaidRequest(DateTime PaidDate, string? Note);
public record UploadSlipRequest( IFormFile Slip  ,string? Note);

