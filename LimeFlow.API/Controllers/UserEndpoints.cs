using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Common.DTOs;
using MediatR;
using LimeFlow.Application.Mediator.Users.Queries;
using LimeFlow.Application.Mediator.Users.Commands;


namespace LimeFlow.API.Controllers
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/v1");

            group.MapGet("/users", async (IMediator mediator) =>
            {
                var response = await mediator.Send(new GetUsersQuery());
                return Results.Ok(response.Select(u => new UserResponseDto(u.Id, u.Name, u.Email, u.CreatedAt)));

            }).WithName("GetUsers")
            .WithSummary("List all registered users")
            .WithDescription("Retrieves a paginated list of all users available in the system. Requires administrative privileges.")
            .WithTags("Users")
            .Produces<List<UserResponseDto>>(StatusCodes.Status200OK, "application/json")
            .Produces(StatusCodes.Status401Unauthorized, typeof(void), "application/json")
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status500InternalServerError, "application/problem+json");


            group.MapGet("/users/{id:guid}", async (Guid id, IMediator mediator) =>
            {
                var response = await mediator.Send(new GetUserByIdQuery(id));
                return Results.Ok(response);

            }).WithName("GetUserById")
              .WithSummary("Get User By Id")
              .WithDescription("Return a user.")
              .WithTags("Users")
              .Produces<UserResponseDto>(StatusCodes.Status200OK)
              .Produces<UserResponseDto>(StatusCodes.Status404NotFound);


            group.MapPost("/users", async (CreateUserCommand request, IMediator mediator) =>
            {

                var response = await mediator.Send(request);
                return Results.Created($"/api/v1/users/{response.Id}", response);

            }).WithName("CreateUser")
            .WithSummary("Create a New User")
            .WithDescription("Creates a new user and returns it with a 201 Created status and Location Header.")
            .WithTags("Users")
            .Produces<UserResponseDto>(StatusCodes.Status201Created)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest);
        }
    }
}
