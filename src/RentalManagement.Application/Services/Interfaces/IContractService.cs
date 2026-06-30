using RentalManagement.Application.Common;
using RentalManagement.Application.DTOs.Contract;

namespace RentalManagement.Application.Services.Interfaces;

public interface IContractService
{
    Task<Result<IEnumerable<ContractDto>>> GetAllAsync();
    Task<Result<ContractDto>> GetByIdAsync(int id);
    Task<Result<ContractDto>> CreateAsync(CreateContractRequest request);
    Task<Result<bool>> DeleteAsync(int id);
    Task<Result<ContractDto>> UpdateAsync(int id, UpdateContractRequest request);
}