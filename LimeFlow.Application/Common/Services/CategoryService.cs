using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Enums;

namespace LimeFlow.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(
        ICategoryRepository categoryRepository,
        ICurrentUserService currentUser,
        IUnitOfWork unitOfWork)
    {
        _categoryRepository = categoryRepository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }

    public Task<IReadOnlyList<CategoryResponse>> GetAllAsync(TransactionType? type, CancellationToken ct = default)
    {
        // TODO(Phase 4): load categories for _currentUser.UserId (optionally filtered by type) and map with ToResponse().
        throw new NotImplementedException();
    }

    public Task<CategoryResponse> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        // TODO(Phase 4): GetByIdAsync(id, userId) or throw NotFoundException(nameof(Category), id).
        throw new NotImplementedException();
    }

    public Task<CategoryResponse> CreateAsync(CreateCategoryRequest request, CancellationToken ct = default)
    {
        // TODO(Phase 4):
        // 1. If ExistsByNameAsync(userId, request.Name) throw ConflictException("A category with this name already exists.").
        // 2. new Category(userId, request.Name, request.Type), Add, CommitAsync, return ToResponse().
        throw new NotImplementedException();
    }

    public Task<CategoryResponse> UpdateAsync(Guid id, UpdateCategoryRequest request, CancellationToken ct = default)
    {
        // TODO(Phase 4):
        // 1. Load the owned category or throw NotFoundException.
        // 2. If ExistsByNameAsync(userId, request.Name, excludeId: id) throw ConflictException.
        // 3. category.Rename(request.Name), CommitAsync, return ToResponse().
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        // TODO(Phase 4):
        // 1. Load the owned category or throw NotFoundException.
        // 2. If IsInUseAsync(id) throw ConflictException("Category is used by transactions or budgets.").
        // 3. Remove and CommitAsync.
        throw new NotImplementedException();
    }
}
