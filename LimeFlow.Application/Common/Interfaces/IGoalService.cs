using LimeFlow.Application.Common.DTOs;

namespace LimeFlow.Application.Common.Interfaces;

public interface IGoalService
{
    Task<IReadOnlyList<GoalResponse>> GetAllAsync(CancellationToken ct = default);
    Task<GoalResponse> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<GoalResponse> CreateAsync(CreateGoalRequest request, CancellationToken ct = default);
    Task<GoalResponse> UpdateAsync(Guid id, UpdateGoalRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
    Task<GoalResponse> ContributeAsync(Guid id, GoalContributionRequest request, CancellationToken ct = default);
    Task<GoalResponse> WithdrawAsync(Guid id, GoalWithdrawalRequest request, CancellationToken ct = default);
}
