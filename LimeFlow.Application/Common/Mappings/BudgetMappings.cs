using LimeFlow.Application.Common.DTOs;
using LimeFlow.Domain.Entities;

namespace LimeFlow.Application.Common.Mappings;

public static class BudgetMappings
{
    public static BudgetResponse ToResponse(this Budget budget) =>
        new(
            budget.Id,
            budget.CategoryId,
            budget.LimitAmount,
            budget.AlertThreshold,
            budget.Period,
            budget.Notes,
            budget.CreatedAt,
            budget.UpdatedAt);
}
