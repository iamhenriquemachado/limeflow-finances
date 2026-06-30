using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;

namespace LimeFlow.API.Controllers
{
    public static class AccountEndpoints
    {
        public static void MapAccountEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/v1");

            group.MapGet("/accounts/{userId:guid}/accounts", async (Guid userId, IAccountService service) =>
            {
                var accounts = await service.GetAllAsync(userId);
                return Results.Ok(accounts);
            }).WithName("GetAllUserAccounts")
              .WithSummary("Get All User Accounts")
              .WithDescription("Retrieves a collection of all financial accounts linked to a specific user ID.")
              .WithTags("Accounts")
              .Produces<IReadOnlyList<AccountSummaryResponseDto>>(StatusCodes.Status200OK)
              .Produces(StatusCodes.Status404NotFound);
        }
    }
}
