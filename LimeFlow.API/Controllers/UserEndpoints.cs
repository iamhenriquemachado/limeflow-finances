using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Services;


namespace LimeFlow.API.Controllers
{
    public static class UserEndpoints
    {

        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/v1");

            group.MapGet("/users", async (UserService service) =>
            {
                var users = await service.GetUsersService();
                return Results.Ok(users);

            }).WithName("GetUsers")
            .WithSummary("List all registered users")
            .WithDescription("Retrieves a paginated list of all users available in the system. Requires administrative privileges.")
            .WithTags("Users")
            .Produces<List<UserResponseDto>>(StatusCodes.Status200OK, "application/json")
            .Produces(StatusCodes.Status401Unauthorized, typeof(void), "application/json")
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError, "application/problem+json");


            group.MapGet("/users/{id:guid}", async (Guid id, UserService service) =>
            {
                var user = await service.GetUserByIdService(id);
                return Results.Ok(user);

            }).WithName("GetUserById")
              .WithSummary("Get User By Id")
              .WithDescription("Return a user.")
              .WithTags("Users")
              .Produces<UserResponseDto>(StatusCodes.Status200OK)
              .Produces<UserResponseDto>(StatusCodes.Status404NotFound);


            group.MapPost("/users", async (CreateUserRequestDto request, UserService service) =>
            {
                
                

            }).WithName("CreateUser")
            .WithSummary("Create a New User")
            .WithDescription("Creates a new user and returns it with a 201 Created status and Location Header.")
            .WithTags("Users")
            .Produces<UserResponseDto>(StatusCodes.Status201Created)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest);

            group.MapDelete("/users", async (Guid id) =>
            {
                //

            });
        }

        
    }
}
