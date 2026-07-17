using RentalManagement.Application.Common;
using RentalManagement.Application.DTOs.Payment;

namespace RentalManagement.Application.Services.Interfaces;

public interface IPaymentService
{
    Task<Result<IEnumerable<PaymentDto>>> GetAllAsync();
    Task<Result<PaymentDto>> GetByIdAsync(int id);
    Task<Result<IEnumerable<PaymentDto>>> GetByContractAsync(int contractId);
    Task<Result<IEnumerable<PaymentDto>>> GetOverdueAsync();
    Task<Result<PaymentDto>> MarkAsPaidAsync(int id, MarkPaidRequest request);
    Task<Result<IEnumerable<PaymentDto>>> GetMyAsync(int userId);
}
