using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace LimeFlow.API.Controllers
{
    public static class AccountEndpoints
    {
        public static void MapAccountEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/v1");

            group.MapGet("/accounts", async (ClaimsPrincipal user, IAccountService service) =>
            {
                var userId = Guid.Parse(user.FindFirst(JwtRegisteredClaimNames.Sub)!.Value);
                var accounts = await service.GetAllAsync(userId);
                return Results.Ok(accounts);

            }).WithName("GetAllUserAccounts")
              .WithSummary("Get All User Accounts")
              .WithDescription("Retrieves a collection of all financial accounts linked to a specific user ID.")
              .WithTags("Accounts")
              .Produces<IReadOnlyList<AccountSummaryResponseDto>>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status404NotFound)
              .RequireAuthorization();

            group.MapPost("/accounts/", async (CreateAccountRequestDto request, IAccountService service) =>
            {
                var createAccount = await service.CreateAsync(request);

                var accountCreatedResponse = new AccountCreatedResponseDto(createAccount.Id, createAccount.Name, createAccount.Bank, createAccount.CreatedAt);
                return Results.Ok(new
                {

                    id = accountCreatedResponse.Id,
                    name = accountCreatedResponse.Name,
                    bank = accountCreatedResponse.Bank,
                    createdAt = accountCreatedResponse.CreatedAt

                });


            }).WithName("CreateNewAccount")
               .WithSummary("Create Account")
               .WithDescription("Create a New User Account.")
               .WithTags("Accounts")
               .Produces<AccountCreatedResponseDto>(StatusCodes.Status200OK)
               .Produces(StatusCodes.Status401Unauthorized)
               .RequireAuthorization();
        }
    }
}
