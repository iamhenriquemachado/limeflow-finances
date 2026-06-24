using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Common.Utils;
using LimeFlow.Domain.Models;
using BCrypt.Net;
using LimeFlow.Application.Common.DTOs;


namespace LimeFlow.API.Controllers
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/v1");

            group.MapGet("/users", async (IUserRepository repo) =>
            {
                var users = await repo.GetAllAsync();


                var userResponseDto = users.Select(user => new UserResponseDto(
                    user.Id,
                    user.Email,
                    user.Name,
                    user.CreatedAt,
                    user.LastUpdatedAt
                 ));

                return Results.Ok(userResponseDto);



            }).WithName("GetUsers")
            .WithSummary("Get All Users")
            .WithDescription("Returns a list of users.")
            .WithTags("Users")
            .Produces<UserResponseDto>(StatusCodes.Status200OK);


            group.MapGet("/user/{id:guid}", async (IUserRepository repo, Guid id) =>
            {
                var errors = new Dictionary<string, string[]>();

                var user = await repo.GetByIdAsync(id);

                if (user is null)
                    errors["id"] = ["The specified user could not be found."];

                if (errors.Count > 0)
                {
                    return Results.ValidationProblem(errors);
                }
                return Results.Ok(user);

            }).WithName("GetUserById")
              .WithSummary("Get User By Id")
              .WithDescription("Return a user.")
              .WithTags("Users")
              .Produces<UserResponseDto>(StatusCodes.Status200OK)
              .Produces<UserResponseDto>(StatusCodes.Status404NotFound);


            group.MapPost("/users", async (CreateUserRequestDto request, IUserRepository repo) =>
            {
                int workFactor = 12;

                var errors = new Dictionary<string, string[]>();

                if (string.IsNullOrWhiteSpace(request.name))
                    errors["name"] = ["Name is required."];
                if (request.name?.Length > 100)
                    errors["name"] = ["Cannot exceed 100 characters for the name"];
                if (string.IsNullOrWhiteSpace(request.password))
                    errors["password"] = ["Password cannot be null or empty"];
                if (Functions.IsValidEmailAddress(request.email) == false)
                    errors["email"] = ["Invalid email address"];
                if (string.IsNullOrEmpty(request.email))
                    errors["email"] = ["Email is empty"];
                

                if (errors.Count > 0)
                {
                    return Results.ValidationProblem(errors);
                }

                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.password, workFactor);

                var newUserEntity = new User
                {
                    Id = Guid.NewGuid(),
                    Email = request.email,
                    Name = request.name,
                    Password = hashedPassword,
                    CreatedAt = DateTime.UtcNow,
                    LastUpdatedAt = DateTime.UtcNow
                };

                var userResponseDto = new UserResponseDto
                (
                    newUserEntity.Id,
                    newUserEntity.Email,
                    newUserEntity.Name,
                    newUserEntity.CreatedAt,
                    newUserEntity.LastUpdatedAt
                );

                await repo.CreateAsync(newUserEntity);

                return Results.Created($"/api/v1/users/{newUserEntity.Id}", userResponseDto);

            }).WithName("CreateUser")
            .WithSummary("Create a New User")
            .WithDescription("Creates a new user and returns it with a 201 Created status and Location Header.")
            .WithTags("Users")
            .Produces<UserResponseDto>(StatusCodes.Status201Created)
            .Produces<HttpValidationProblemDetails>(StatusCodes.Status400BadRequest);
        }
    }
}
