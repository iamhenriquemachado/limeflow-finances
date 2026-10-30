using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Interfaces;

public interface IAccountService
{
    Task<IReadOnlyList<AccountResponse>> GetAllAsync(CancellationToken ct = default);
    Task<AccountResponse> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<AccountResponse> CreateAsync(CreateAccountRequest request, CancellationToken ct = default);
    Task<AccountResponse> UpdateAsync(Guid id, UpdateAccountRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
