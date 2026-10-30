using LimeFlow.API.Infrastructure;
using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Enums;
using Microsoft.AspNetCore.Http.HttpResults;

namespace LimeFlow.API.Endpoints;

public static class CategoryEndpoints
{
    public static IEndpointRouteBuilder MapCategoryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/v1/categories")
            .WithTags("Categories")
            .RequireAuthorization()
            .ProducesProblem(StatusCodes.Status401Unauthorized);

        group.MapGet("/", GetAll)
            .WithName("GetCategories")
            .WithSummary("List the authenticated user's categories");

        group.MapGet("/{id:guid}", GetById)
            .WithName("GetCategoryById")
            .WithSummary("Get a category by id")
            .ProducesProblem(StatusCodes.Status404NotFound);

        group.MapPost("/", Create)
            .WithName("CreateCategory")
            .WithSummary("Create a category")
            .WithValidation<CreateCategoryRequest>()
            .ProducesProblem(StatusCodes.Status409Conflict);

        group.MapPut("/{id:guid}", Update)
            .WithName("UpdateCategory")
            .WithSummary("Rename a category")
            .WithValidation<UpdateCategoryRequest>()
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict);

        group.MapDelete("/{id:guid}", Delete)
            .WithName("DeleteCategory")
            .WithSummary("Delete a category")
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status409Conflict);

        return app;
    }

    private static async Task<Ok<IReadOnlyList<CategoryResponse>>> GetAll(
        TransactionType? type,
        ICategoryService categoryService,
        CancellationToken ct)
    {
        var categories = await categoryService.GetAllAsync(type, ct);
        return TypedResults.Ok(categories);
    }

    private static async Task<Ok<CategoryResponse>> GetById(Guid id, ICategoryService categoryService, CancellationToken ct)
    {
        var category = await categoryService.GetByIdAsync(id, ct);
        return TypedResults.Ok(category);
    }

    private static async Task<Created<CategoryResponse>> Create(
        CreateCategoryRequest request,
        ICategoryService categoryService,
        CancellationToken ct)
    {
        var category = await categoryService.CreateAsync(request, ct);
        return TypedResults.Created($"/api/v1/categories/{category.Id}", category);
    }

    private static async Task<Ok<CategoryResponse>> Update(
        Guid id,
        UpdateCategoryRequest request,
        ICategoryService categoryService,
        CancellationToken ct)
    {
        var category = await categoryService.UpdateAsync(id, request, ct);
        return TypedResults.Ok(category);
    }

    private static async Task<NoContent> Delete(Guid id, ICategoryService categoryService, CancellationToken ct)
    {
        await categoryService.DeleteAsync(id, ct);
        return TypedResults.NoContent();
    }
}
