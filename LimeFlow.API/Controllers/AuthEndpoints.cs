using LimeFlow.Application.Common.DTOs;
using LimeFlow.Infrastructure.Auth;

namespace LimeFlow.API.Controllers
{
    public static class AuthEndpoints
    {

        public static void MapAuthEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/v1/");

            group.MapPost("/sessions", async (LoginRequestDto request, ITokenService service) =>
            {

                var token = service.GenerateToken(request);

                return token == null ? Results.Unauthorized() : Results.Ok(new
                {
                    accessToken = token,
                    tokenType = "Bearer",
                    expiresIn = 3600
                });

            }).WithName("Login")
            .WithSummary("Authenticates the user and returns a token")
            .WithDescription("Validates user credentials and returns a JWT for accessing protected resources.")
            .WithTags("Authentication")
            .Produces<LoginResponseDto>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status401Unauthorized);
        }
    }
}
