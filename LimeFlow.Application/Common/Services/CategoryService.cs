using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Exceptions;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Common.Mappings;
using LimeFlow.Domain.Entities;
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

    public async Task<IReadOnlyList<CategoryResponse>> GetAllAsync(TransactionType? type, CancellationToken ct = default)
    {
        var categories = await _categoryRepository.GetAllAsync(_currentUser.UserId, type, ct);
        return categories.Select(c => c.ToResponse()).ToList();
    }

    public async Task<CategoryResponse> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var category = await GetOwnedCategoryAsync(id, ct);
        return category.ToResponse();
    }

    public async Task<CategoryResponse> CreateAsync(CreateCategoryRequest request, CancellationToken ct = default)
    {
        if (await _categoryRepository.ExistsByNameAsync(_currentUser.UserId, request.Name, ct: ct))
            throw new ConflictException("A category with this name already exists.");

        var category = new Category(_currentUser.UserId, request.Name, request.Type);

        _categoryRepository.Add(category);
        await _unitOfWork.CommitAsync(ct);

        return category.ToResponse();
    }

    public async Task<CategoryResponse> UpdateAsync(Guid id, UpdateCategoryRequest request, CancellationToken ct = default)
    {
        var category = await GetOwnedCategoryAsync(id, ct);

        if (await _categoryRepository.ExistsByNameAsync(_currentUser.UserId, request.Name, excludeId: id, ct))
            throw new ConflictException("A category with this name already exists.");

        category.Rename(request.Name);
        await _unitOfWork.CommitAsync(ct);

        return category.ToResponse();
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var category = await GetOwnedCategoryAsync(id, ct);

        if (await _categoryRepository.IsInUseAsync(id, ct))
            throw new ConflictException("Category is used by transactions or budgets.");

        _categoryRepository.Remove(category);
        await _unitOfWork.CommitAsync(ct);
    }

    private async Task<Category> GetOwnedCategoryAsync(Guid id, CancellationToken ct) =>
        await _categoryRepository.GetByIdAsync(id, _currentUser.UserId, ct)
        ?? throw new NotFoundException(nameof(Category), id);
}
