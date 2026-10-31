using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;

namespace LimeFlow.Application.Services;

public class BudgetService : IBudgetService
{
    private readonly IBudgetRepository _budgetRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public BudgetService(
        IBudgetRepository budgetRepository,
        ICategoryRepository categoryRepository,
        ICurrentUserService currentUser,
        IUnitOfWork unitOfWork)
    {
        _budgetRepository = budgetRepository;
        _categoryRepository = categoryRepository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }

    public Task<IReadOnlyList<BudgetResponse>> GetAllAsync(CancellationToken ct = default)
    {
        // TODO(Phase 6): load budgets for _currentUser.UserId and map with ToResponse().
        throw new NotImplementedException();
    }

    public Task<BudgetResponse> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        // TODO(Phase 6): GetByIdAsync(id, userId) or throw NotFoundException(nameof(Budget), id).
        throw new NotImplementedException();
    }

    public Task<BudgetResponse> CreateAsync(CreateBudgetRequest request, CancellationToken ct = default)
    {
        // TODO(Phase 6):
        // 1. Load the owned category or throw NotFoundException.
        // 2. If ExistsForCategoryAndPeriodAsync(userId, categoryId, period) throw ConflictException.
        // 3. new Budget(...), Add, CommitAsync, return ToResponse().
        throw new NotImplementedException();
    }

    public Task<BudgetResponse> UpdateAsync(Guid id, UpdateBudgetRequest request, CancellationToken ct = default)
    {
        // TODO(Phase 6):
        // 1. Load the owned budget or throw NotFoundException.
        // 2. Load the owned category or throw NotFoundException.
        // 3. If the category/period pair changed, check ExistsForCategoryAndPeriodAsync(..., excludeId: id).
        // 4. budget.Update(...), CommitAsync, return ToResponse().
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        // TODO(Phase 6): load owned budget, Remove, CommitAsync.
        throw new NotImplementedException();
    }

    public Task<BudgetStatusResponse> GetStatusAsync(Guid id, CancellationToken ct = default)
    {
        // TODO(Phase 6):
        // 1. Load the owned budget or throw NotFoundException.
        // 2. Compute periodStart/periodEnd with a pure function of budget.Period and DateTime.UtcNow (unit-testable).
        // 3. spent = sum of Expense transactions in that category within the period (AsNoTracking projection).
        // 4. remaining = limit - spent; percentUsed = round(spent/limit*100);
        //    isOverThreshold = percentUsed >= AlertThreshold; isExceeded = spent > limit.
        throw new NotImplementedException();
    }
}
