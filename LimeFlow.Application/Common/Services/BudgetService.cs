using LimeFlow.Application.Common;
using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Exceptions;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Common.Mappings;
using LimeFlow.Domain.Entities;

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

    public async Task<IReadOnlyList<BudgetResponse>> GetAllAsync(CancellationToken ct = default)
    {
        var budgets = await _budgetRepository.GetAllAsync(_currentUser.UserId, ct);
        return budgets.Select(b => b.ToResponse()).ToList();
    }

    public async Task<BudgetResponse> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var budget = await GetOwnedBudgetAsync(id, ct);
        return budget.ToResponse();
    }

    public async Task<BudgetResponse> CreateAsync(CreateBudgetRequest request, CancellationToken ct = default)
    {
        await GetOwnedCategoryAsync(request.CategoryId, ct);

        if (await _budgetRepository.ExistsForCategoryAndPeriodAsync(
                _currentUser.UserId, request.CategoryId, request.Period, ct: ct))
            throw new ConflictException("A budget for this category and period already exists.");

        var budget = new Budget(
            _currentUser.UserId,
            request.CategoryId,
            request.LimitAmount,
            request.AlertThreshold,
            request.Period,
            request.Notes);

        _budgetRepository.Add(budget);
        await _unitOfWork.CommitAsync(ct);
        return budget.ToResponse();
    }

    public async Task<BudgetResponse> UpdateAsync(Guid id, UpdateBudgetRequest request, CancellationToken ct = default)
    {
        var budget = await GetOwnedBudgetAsync(id, ct);
        await GetOwnedCategoryAsync(request.CategoryId, ct);

        if (await _budgetRepository.ExistsForCategoryAndPeriodAsync(
                _currentUser.UserId, request.CategoryId, request.Period, excludeId: id, ct))
            throw new ConflictException("A budget for this category and period already exists.");

        budget.Update(request.CategoryId, request.LimitAmount, request.AlertThreshold, request.Period, request.Notes);
        await _unitOfWork.CommitAsync(ct);
        return budget.ToResponse();
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var budget = await GetOwnedBudgetAsync(id, ct);
        _budgetRepository.Remove(budget);
        await _unitOfWork.CommitAsync(ct);
    }

    public async Task<BudgetStatusResponse> GetStatusAsync(Guid id, CancellationToken ct = default)
    {
        var budget = await GetOwnedBudgetAsync(id, ct);
        var (periodStart, periodEnd) = BudgetPeriods.GetCurrentRange(budget.Period, DateTime.UtcNow);
        var spent = await _budgetRepository.SumExpensesAsync(
            _currentUser.UserId, budget.CategoryId, periodStart, periodEnd, ct);

        var percentUsed = budget.LimitAmount == 0
            ? 0
            : (int)Math.Round(spent / budget.LimitAmount * 100m, MidpointRounding.AwayFromZero);

        return new BudgetStatusResponse(
            budget.LimitAmount,
            spent,
            budget.LimitAmount - spent,
            percentUsed,
            percentUsed >= budget.AlertThreshold,
            spent > budget.LimitAmount,
            periodStart,
            periodEnd);
    }

    private async Task<Budget> GetOwnedBudgetAsync(Guid id, CancellationToken ct) =>
        await _budgetRepository.GetByIdAsync(id, _currentUser.UserId, ct)
        ?? throw new NotFoundException(nameof(Budget), id);

    private async Task GetOwnedCategoryAsync(Guid id, CancellationToken ct)
    {
        _ = await _categoryRepository.GetByIdAsync(id, _currentUser.UserId, ct)
            ?? throw new NotFoundException(nameof(Category), id);
    }
}
