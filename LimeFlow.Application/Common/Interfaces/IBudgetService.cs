using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Interfaces;

public interface IBudgetService
{
    Task<IReadOnlyList<BudgetResponse>> GetAllAsync(CancellationToken ct = default);
    Task<BudgetResponse> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<BudgetResponse> CreateAsync(CreateBudgetRequest request, CancellationToken ct = default);
    Task<BudgetResponse> UpdateAsync(Guid id, UpdateBudgetRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task<BudgetStatusResponse> GetStatusAsync(Guid id, CancellationToken ct = default);
}
