using LimeFlow.API.Infrastructure;
using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Common.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LimeFlow.API.Endpoints;

public static class TransactionEndpoints
{
    public static IEndpointRouteBuilder MapTransactionEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/transactions")
            .WithTags("Transactions")
            .RequireAuthorization()
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        group.MapGet("/", GetAll)
            .WithName("GetTransactions")
            .WithSummary("List the authenticated user's transactions")
            .WithDescription("Supports filters (accountId, categoryId, type, from, to) and paging (page, pageSize).");

        group.MapGet("/{id:guid}", GetById)
            .WithName("GetTransactionById")
            .WithSummary("Get a transaction by id")
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/", Create)
            .WithName("CreateTransaction")
            .WithSummary("Create a transaction and update the account balance")
            .WithValidation<CreateTransactionRequest>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity);

        group.MapPut("/{id:guid}", Update)
            .WithName("UpdateTransaction")
            .WithSummary("Update a transaction (reverts the old effect and applies the new one)")
            .WithValidation<UpdateTransactionRequest>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity);

        group.MapDelete("/{id:guid}", Delete)
            .WithName("DeleteTransaction")
            .WithSummary("Delete a transaction and reverse its effect on the account balance")
            .ProducesProblem(StatusCodes.Status404NotFound);

        return app;
    }

    private static async Task<Ok<PagedResponse<TransactionResponse>>> GetAll(
        [AsParameters] TransactionFilter filter,
        ITransactionService transactionService,
        CancellationToken ct)
    {
        var page = await transactionService.GetAllAsync(filter, ct);
        return TypedResults.Ok(page);
    }

    private static async Task<Ok<TransactionResponse>> GetById(Guid id, ITransactionService transactionService, CancellationToken ct)
    {
        var transaction = await transactionService.GetByIdAsync(id, ct);
        return TypedResults.Ok(transaction);
    }

    private static async Task<Created<TransactionResponse>> Create(
        CreateTransactionRequest request,
        ITransactionService transactionService,
        CancellationToken ct)
    {
        var transaction = await transactionService.CreateAsync(request, ct);
        return TypedResults.Created($"/api/v1/transactions/{transaction.Id}", transaction);
    }

    private static async Task<Ok<TransactionResponse>> Update(
        Guid id,
        UpdateTransactionRequest request,
        ITransactionService transactionService,
        CancellationToken ct)
    {
        var transaction = await transactionService.UpdateAsync(id, request, ct);
        return TypedResults.Ok(transaction);
    }

    private static async Task<NoContent> Delete(Guid id, ITransactionService transactionService, CancellationToken ct)
    {
        await transactionService.DeleteAsync(id, ct);
        return TypedResults.NoContent();
    }
}
