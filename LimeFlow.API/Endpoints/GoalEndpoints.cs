using LimeFlow.API.Infrastructure;
using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LimeFlow.API.Endpoints;

public static class GoalEndpoints
{
    public static IEndpointRouteBuilder MapGoalEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/goals")
            .WithTags("Goals")
            .RequireAuthorization()
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        group.MapGet("/", GetAll)
            .WithName("GetGoals")
            .WithSummary("List the authenticated user's savings goals");

        group.MapGet("/{id:guid}", GetById)
            .WithName("GetGoalById")
            .WithSummary("Get a goal by id, including progress")
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/", Create)
            .WithName("CreateGoal")
            .WithSummary("Create a savings goal")
            .WithValidation<CreateGoalRequest>();

        group.MapPut("/{id:guid}", Update)
            .WithName("UpdateGoal")
            .WithSummary("Update a savings goal")
            .WithValidation<UpdateGoalRequest>()
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:guid}", Delete)
            .WithName("DeleteGoal")
            .WithSummary("Delete a savings goal")
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/{id:guid}/contributions", Contribute)
            .WithName("ContributeToGoal")
            .WithSummary("Add money to a goal")
            .WithValidation<GoalContributionRequest>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity);

        group.MapPost("/{id:guid}/withdrawals", Withdraw)
            .WithName("WithdrawFromGoal")
            .WithSummary("Withdraw money from a goal")
            .WithValidation<GoalWithdrawalRequest>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity);

        return app;
    }

    private static async Task<Ok<IReadOnlyList<GoalResponse>>> GetAll(IGoalService goalService, CancellationToken ct)
    {
        var goals = await goalService.GetAllAsync(ct);
        return TypedResults.Ok(goals);
    }

    private static async Task<Ok<GoalResponse>> GetById(Guid id, IGoalService goalService, CancellationToken ct)
    {
        var goal = await goalService.GetByIdAsync(id, ct);
        return TypedResults.Ok(goal);
    }

    private static async Task<Created<GoalResponse>> Create(
        CreateGoalRequest request,
        IGoalService goalService,
        CancellationToken ct)
    {
        var goal = await goalService.CreateAsync(request, ct);
        return TypedResults.Created($"/api/v1/goals/{goal.Id}", goal);
    }

    private static async Task<Ok<GoalResponse>> Update(
        Guid id,
        UpdateGoalRequest request,
        IGoalService goalService,
        CancellationToken ct)
    {
        var goal = await goalService.UpdateAsync(id, request, ct);
        return TypedResults.Ok(goal);
    }

    private static async Task<NoContent> Delete(Guid id, IGoalService goalService, CancellationToken ct)
    {
        await goalService.DeleteAsync(id, ct);
        return TypedResults.NoContent();
    }

    private static async Task<Ok<GoalResponse>> Contribute(
        Guid id,
        GoalContributionRequest request,
        IGoalService goalService,
        CancellationToken ct)
    {
        var goal = await goalService.ContributeAsync(id, request, ct);
        return TypedResults.Ok(goal);
    }

    private static async Task<Ok<GoalResponse>> Withdraw(
        Guid id,
        GoalWithdrawalRequest request,
        IGoalService goalService,
        CancellationToken ct)
    {
        var goal = await goalService.WithdrawAsync(id, request, ct);
        return TypedResults.Ok(goal);
    }
}
