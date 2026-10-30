using LimeFlow.API.Infrastructure;
using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LimeFlow.API.Endpoints;

public static class AccountEndpoints
{
    public static IEndpointRouteBuilder MapAccountEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/accounts")
            .WithTags("Accounts")
            .RequireAuthorization()
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        group.MapGet("/", GetAll)
            .WithName("GetAccounts")
            .WithSummary("List the authenticated user's accounts");

        group.MapGet("/{id:guid}", GetById)
            .WithName("GetAccountById")
            .WithSummary("Get an account by id")
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/", Create)
            .WithName("CreateAccount")
            .WithSummary("Create an account (balance starts at 0)")
            .WithValidation<CreateAccountRequest>();

        group.MapPut("/{id:guid}", Update)
            .WithName("UpdateAccount")
            .WithSummary("Update an account's name and bank")
            .WithDescription("The balance is never editable; it only changes through transactions.")
            .WithValidation<UpdateAccountRequest>()
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapDelete("/{id:guid}", Delete)
            .WithName("DeleteAccount")
            .WithSummary("Delete an account")
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict);

        return app;
    }

    private static async Task<Ok<IReadOnlyList<AccountResponse>>> GetAll(
        IAccountService accountService,
        CancellationToken ct)
    {
        var accounts = await accountService.GetAllAsync(ct);
        return TypedResults.Ok(accounts);
    }

    private static async Task<Ok<AccountResponse>> GetById(
        Guid id,
        IAccountService accountService,
        CancellationToken ct)
    {
        var account = await accountService.GetByIdAsync(id, ct);
        return TypedResults.Ok(account);
    }

    private static async Task<Created<AccountResponse>> Create(
        CreateAccountRequest request,
        IAccountService accountService,
        CancellationToken ct)
    {
        var account = await accountService.CreateAsync(request, ct);
        return TypedResults.Created($"/api/v1/accounts/{account.Id}", account);
    }

    private static async Task<Ok<AccountResponse>> Update(
        Guid id,
        UpdateAccountRequest request,
        IAccountService accountService,
        CancellationToken ct)
    {
        var account = await accountService.UpdateAsync(id, request, ct);
        return TypedResults.Ok(account);
    }

    private static async Task<NoContent> Delete(
        Guid id,
        IAccountService accountService,
        CancellationToken ct)
    {
        await accountService.DeleteAsync(id, ct);
        return TypedResults.NoContent();
    }
}
