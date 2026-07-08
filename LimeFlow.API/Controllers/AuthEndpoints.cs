using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Services;
using LimeFlow.Infrastructure.Auth;
using Microsoft.AspNetCore.Mvc;

namespace LimeFlow.API.Controllers
{
    public static class AuthEndpoints
    {

        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/v1/");

            group.MapPost("/sessions", async (LoginRequestDto request, [FromServices] IAuthService service, CancellationToken ct) =>
            {

                var loginToken = await service.LoginAsync(request, ct);


                return loginToken == null ? Results.Unauthorized() : Results.Ok(new
                {
                    accessToken = loginToken.accessToken,
                    tokenType = "Bearer",
                    expiresIn = 3600
                });

            }).WithName("Login")
            .WithSummary("User Authentication")
            .WithDescription("Validates user credentials and returns a JWT for accessing protected resources.")
            .WithTags("Authentication")
            .Produces<LoginResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
        }
    }
}
