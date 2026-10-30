using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Models;

namespace LimeFlow.Application.Common.Interfaces;

public interface ITransactionService
{
    Task<PagedResponse<TransactionResponse>> GetAllAsync(TransactionFilter filter, CancellationToken ct = default);
    Task<TransactionResponse> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<TransactionResponse> CreateAsync(CreateTransactionRequest request, CancellationToken ct = default);
    Task<TransactionResponse> UpdateAsync(Guid id, UpdateTransactionRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
