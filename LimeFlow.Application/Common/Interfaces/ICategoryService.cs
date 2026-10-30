using LimeFlow.Application.Common.DTOs;
using LimeFlow.Domain.Enums;

namespace LimeFlow.Application.Common.Interfaces;

public interface ICategoryService
{
    Task<IReadOnlyList<CategoryResponse>> GetAllAsync(TransactionType? type, CancellationToken ct = default);
    Task<CategoryResponse> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<CategoryResponse> CreateAsync(CreateCategoryRequest request, CancellationToken ct = default);
    Task<CategoryResponse> UpdateAsync(Guid id, UpdateCategoryRequest request, CancellationToken ct = default);
    Task DeleteAsync(Guid id, CancellationToken ct = default);
}
