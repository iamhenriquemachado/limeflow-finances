using LimeFlow.API.Infrastructure;
using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LimeFlow.API.Endpoints;

public static class BudgetEndpoints
{
    public static IEndpointRouteBuilder MapBudgetEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/budgets")
            .WithTags("Budgets")
            .RequireAuthorization()
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        group.MapGet("/", GetAll)
            .WithName("GetBudgets")
            .WithSummary("List the authenticated user's budgets");

        group.MapGet("/{id:guid}", GetById)
            .WithName("GetBudgetById")
            .WithSummary("Get a budget by id")
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapGet("/{id:guid}/status", GetStatus)
            .WithName("GetBudgetStatus")
            .WithSummary("Get spent vs limit for the current period")
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/", Create)
            .WithName("CreateBudget")
            .WithSummary("Create a budget for a category and period")
            .WithValidation<CreateBudgetRequest>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict);

        group.MapPut("/{id:guid}", Update)
            .WithName("UpdateBudget")
            .WithSummary("Update a budget")
            .WithValidation<UpdateBudgetRequest>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict);

        group.MapDelete("/{id:guid}", Delete)
            .WithName("DeleteBudget")
            .WithSummary("Delete a budget")
            .ProducesProblem(StatusCodes.Status404NotFound);

        return app;
    }

    private static async Task<Ok<IReadOnlyList<BudgetResponse>>> GetAll(IBudgetService budgetService, CancellationToken ct)
    {
        var budgets = await budgetService.GetAllAsync(ct);
        return TypedResults.Ok(budgets);
    }

    private static async Task<Ok<BudgetResponse>> GetById(Guid id, IBudgetService budgetService, CancellationToken ct)
    {
        var budget = await budgetService.GetByIdAsync(id, ct);
        return TypedResults.Ok(budget);
    }

    private static async Task<Ok<BudgetStatusResponse>> GetStatus(Guid id, IBudgetService budgetService, CancellationToken ct)
    {
        var status = await budgetService.GetStatusAsync(id, ct);
        return TypedResults.Ok(status);
    }

    private static async Task<Created<BudgetResponse>> Create(
        CreateBudgetRequest request,
        IBudgetService budgetService,
        CancellationToken ct)
    {
        var budget = await budgetService.CreateAsync(request, ct);
        return TypedResults.Created($"/api/v1/budgets/{budget.Id}", budget);
    }

    private static async Task<Ok<BudgetResponse>> Update(
        Guid id,
        UpdateBudgetRequest request,
        IBudgetService budgetService,
        CancellationToken ct)
    {
        var budget = await budgetService.UpdateAsync(id, request, ct);
        return TypedResults.Ok(budget);
    }

    private static async Task<NoContent> Delete(Guid id, IBudgetService budgetService, CancellationToken ct)
    {
        await budgetService.DeleteAsync(id, ct);
        return TypedResults.NoContent();
    }
}
