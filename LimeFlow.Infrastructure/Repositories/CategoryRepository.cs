using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Entities;
using LimeFlow.Domain.Enums;
using LimeFlow.Infrastructure.Persistence;

namespace LimeFlow.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Category?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct = default)
    {
        // TODO(Phase 4): FirstOrDefaultAsync where Id == id && UserId == userId.
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Category>> GetAllAsync(Guid userId, TransactionType? type, CancellationToken ct = default)
    {
        // TODO(Phase 4): AsNoTracking, filter by userId and optional type, OrderBy Name.
        throw new NotImplementedException();
    }

    public Task<bool> ExistsByNameAsync(Guid userId, string name, Guid? excludeId = null, CancellationToken ct = default)
    {
        // TODO(Phase 4): AnyAsync on UserId + Name (case-insensitive), excluding excludeId when set.
        throw new NotImplementedException();
    }

    public Task<bool> IsInUseAsync(Guid id, CancellationToken ct = default)
    {
        // TODO(Phase 4): true when any Transaction or Budget references this category id.
        throw new NotImplementedException();
    }

    public void Add(Category category) => _context.Categories.Add(category);

    public void Remove(Category category) => _context.Categories.Remove(category);
}
