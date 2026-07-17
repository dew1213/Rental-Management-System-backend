using Microsoft.EntityFrameworkCore;
using RentalManagement.Domain.Entities;
using RentalManagement.Domain.Interfaces;
using RentalManagement.Infrastructure.Data;

namespace RentalManagement.Infrastructure.Repositories;

public class PaymentRepository
    : GenericRepository<Payment>,
      IPaymentRepository
{
    private readonly AppDbContext _context;

    public PaymentRepository(AppDbContext context)
        : base(context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Payment>> GetByContractIdAsync(int contractId)
    {
        return await _context.Payments
            .Where(x => x.ContractId == contractId)
            .OrderByDescending(x => x.DueDate)
            .ToListAsync();
    }
}