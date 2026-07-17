using RentalManagement.Application.Common;
using RentalManagement.Application.DTOs.Payment;
using RentalManagement.Application.Services.Interfaces;
using RentalManagement.Domain.Entities;
using RentalManagement.Domain.Enums;
using RentalManagement.Domain.Interfaces;

namespace RentalManagement.Application.Services;

public class PaymentService : IPaymentService
{
    private readonly IUnitOfWork _unitOfWork;

    public PaymentService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<IEnumerable<PaymentDto>>> GetAllAsync()
    {
        var payments = await _unitOfWork.Payments.GetAllAsync();

        var result = payments.Select(x => new PaymentDto(
            x.Id,
            x.ContractId,
            x.Amount,
            x.DueDate,
            x.PaidDate,
            x.Status,
            x.Note
        ));

        return Result<IEnumerable<PaymentDto>>.Success(result);
    }

    public async Task<Result<PaymentDto>> GetByIdAsync(int id)
    {
        var payment = await _unitOfWork.Payments.GetByIdAsync(id);

        if (payment == null)
            return Result<PaymentDto>.Failure("Payment not found.");

        return Result<PaymentDto>.Success(new PaymentDto(
            payment.Id,
            payment.ContractId,
            payment.Amount,
            payment.DueDate,
            payment.PaidDate,
            payment.Status,
            payment.Note
        ));
    }

    public async Task<Result<IEnumerable<PaymentDto>>> GetByContractAsync(int contractId)
    {
        var payments = await _unitOfWork.Payments.FindAsync(x => x.ContractId == contractId);

        var result = payments.Select(x => new PaymentDto(
            x.Id,
            x.ContractId,
            x.Amount,
            x.DueDate,
            x.PaidDate,
            x.Status,
            x.Note
        ));

        return Result<IEnumerable<PaymentDto>>.Success(result);
    }

    public async Task<Result<IEnumerable<PaymentDto>>> GetOverdueAsync()
    {
        var payments = await _unitOfWork.Payments.FindAsync(x => x.Status == PaymentStatus.Overdue);

        var result = payments.Select(x => new PaymentDto(
            x.Id,
            x.ContractId,
            x.Amount,
            x.DueDate,
            x.PaidDate,
            x.Status,
            x.Note
        ));

        return Result<IEnumerable<PaymentDto>>.Success(result);
    }

    public async Task<Result<PaymentDto>> MarkAsPaidAsync(int id, MarkPaidRequest request)
    {
        var payment = await _unitOfWork.Payments.GetByIdAsync(id);

        if (payment == null)
            return Result<PaymentDto>.Failure("Payment not found.");

        payment.Status = PaymentStatus.Paid;
        payment.PaidDate = request.PaidDate.ToUniversalTime();
        payment.Note = request.Note;

        _unitOfWork.Payments.Update(payment);

        await _unitOfWork.SaveChangesAsync();

        return Result<PaymentDto>.Success(new PaymentDto(
            payment.Id,
            payment.ContractId,
            payment.Amount,
            payment.DueDate,
            payment.PaidDate,
            payment.Status,
            payment.Note
        ));
    }

    public async Task<Result<IEnumerable<PaymentDto>>> GetMyAsync(int userId)
    {
        var contract = await _unitOfWork.Contracts.GetByIdWithDetailsAsync(userId);

        if (contract == null)
            return Result<IEnumerable<PaymentDto>>
                .Failure("Contract not found.");

        var payments = await _unitOfWork.Payments
            .GetByContractIdAsync(userId);

        var result = payments.Select(p => new PaymentDto(
            p.Id,
            p.ContractId,
            p.Amount,
            p.DueDate,
            p.PaidDate,
            p.Status,
            p.Note
        ));

        return Result<IEnumerable<PaymentDto>>
            .Success(result);
    }
}