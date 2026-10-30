using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;

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

    public Task<IReadOnlyList<GoalResponse>> GetAllAsync(CancellationToken ct = default)
    {
        // TODO(Phase 7): load goals for _currentUser.UserId and map with ToResponse().
        throw new NotImplementedException();
    }

    public Task<GoalResponse> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        // TODO(Phase 7): GetByIdAsync(id, userId) or throw NotFoundException(nameof(Goal), id).
        throw new NotImplementedException();
    }

    public Task<GoalResponse> CreateAsync(CreateGoalRequest request, CancellationToken ct = default)
    {
        // TODO(Phase 7): new Goal(userId, ...), Add, CommitAsync, return ToResponse().
        throw new NotImplementedException();
    }

    public Task<GoalResponse> UpdateAsync(Guid id, UpdateGoalRequest request, CancellationToken ct = default)
    {
        // TODO(Phase 7): load owned goal, goal.Update(...), CommitAsync, return ToResponse().
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        // TODO(Phase 7): load owned goal, Remove, CommitAsync.
        throw new NotImplementedException();
    }

    public Task<GoalResponse> ContributeAsync(Guid id, GoalContributionRequest request, CancellationToken ct = default)
    {
        // TODO(Phase 7):
        // 1. Load the owned goal or throw NotFoundException.
        // 2. goal.Contribute(request.Amount).
        // 3. If FromAccountId is set: load the owned account (404 if missing), account.Debit(amount).
        //    Optionally create an Expense transaction in the same commit.
        // 4. Single CommitAsync, return ToResponse().
        throw new NotImplementedException();
    }

    public Task<GoalResponse> WithdrawAsync(Guid id, GoalWithdrawalRequest request, CancellationToken ct = default)
    {
        // TODO(Phase 7): load owned goal, goal.Withdraw(request.Amount) (DomainException if amount > AlreadySaved),
        // CommitAsync, return ToResponse().
        throw new NotImplementedException();
    }
}
