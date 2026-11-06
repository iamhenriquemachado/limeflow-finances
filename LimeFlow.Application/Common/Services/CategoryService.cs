using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Common.Mappings;
using LimeFlow.Domain.Enums;
using LimeFlow.Application.Common.Exceptions;
using LimeFlow.Domain.Entities;

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

    public async Task<IReadOnlyList<CategoryResponse>> GetAllAsync(TransactionType? type, CancellationToken ct = default)
    {
        var categories = await _categoryRepository.GetAllAsync(_currentUser.UserId, type, ct);
        return categories.Select(c => c.ToResponse()).ToList();
    }

    public async Task<CategoryResponse> GetByIdAsync(Guid id, CancellationToken ct = default)
    {

        var category = await _categoryRepository.GetByIdAsync(id, _currentUser.UserId, ct);

        if (category == null)
            throw new NotFoundException(nameof(Category), id);
        return category.ToResponse();
        
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
