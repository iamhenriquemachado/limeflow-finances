using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Exceptions;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Common.Mappings;
using LimeFlow.Domain.Entities;

namespace LimeFlow.Application.Services;

public class GoalService : IGoalService
{
    private readonly IGoalRepository _goalRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public GoalService(
        IGoalRepository goalRepository,
        IAccountRepository accountRepository,
        ICurrentUserService currentUser,
        IUnitOfWork unitOfWork)
    {
        _goalRepository = goalRepository;
        _accountRepository = accountRepository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<GoalResponse>> GetAllAsync(CancellationToken ct = default)
    {
        var goals = await _goalRepository.GetAllAsync(_currentUser.UserId, ct);
        return goals.Select(g => g.ToResponse()).ToList();
    }

    public async Task<GoalResponse> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var goal = await GetOwnedGoalAsync(id, ct);
        return goal.ToResponse();
    }

    public async Task<GoalResponse> CreateAsync(CreateGoalRequest request, CancellationToken ct = default)
    {
        var goal = new Goal(
            _currentUser.UserId,
            request.Name,
            request.Description,
            request.Target,
            request.TargetDate,
            request.MonthlyContribution,
            request.Icon);

        _goalRepository.Add(goal);
        await _unitOfWork.CommitAsync(ct);
        return goal.ToResponse();
    }

    public async Task<GoalResponse> UpdateAsync(Guid id, UpdateGoalRequest request, CancellationToken ct = default)
    {
        var goal = await GetOwnedGoalAsync(id, ct);

        goal.Update(
            request.Name,
            request.Description,
            request.Target,
            request.TargetDate,
            request.MonthlyContribution,
            request.Icon);

        await _unitOfWork.CommitAsync(ct);
        return goal.ToResponse();
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var goal = await GetOwnedGoalAsync(id, ct);
        _goalRepository.Remove(goal);
        await _unitOfWork.CommitAsync(ct);
    }

    public async Task<GoalResponse> ContributeAsync(Guid id, GoalContributionRequest request, CancellationToken ct = default)
    {
        var goal = await GetOwnedGoalAsync(id, ct);

        Account? account = null;
        if (request.FromAccountId is Guid accountId)
        {
            account = await _accountRepository.GetByIdAsync(accountId, _currentUser.UserId, ct)
                ?? throw new NotFoundException(nameof(Account), accountId);
        }

        goal.Contribute(request.Amount);
        account?.Debit(request.Amount);

        await _unitOfWork.CommitAsync(ct);
        return goal.ToResponse();
    }

    public async Task<GoalResponse> WithdrawAsync(Guid id, GoalWithdrawalRequest request, CancellationToken ct = default)
    {
        var goal = await GetOwnedGoalAsync(id, ct);
        goal.Withdraw(request.Amount);
        await _unitOfWork.CommitAsync(ct);
        return goal.ToResponse();
    }

    private async Task<Goal> GetOwnedGoalAsync(Guid id, CancellationToken ct) =>
        await _goalRepository.GetByIdAsync(id, _currentUser.UserId, ct)
        ?? throw new NotFoundException(nameof(Goal), id);
}
