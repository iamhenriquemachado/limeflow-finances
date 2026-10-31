using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Entities;
using LimeFlow.Domain.Enums;
using LimeFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LimeFlow.Infrastructure.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Category?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct = default) =>
        _context.Categories.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId, ct);

    public async Task<IReadOnlyList<Category>> GetAllAsync(Guid userId, TransactionType? type, CancellationToken ct = default) =>
        await _context.Categories
            .AsNoTracking()
            .Where(c => c.UserId == userId)
            .OrderBy(c => c.Name)
            .ToListAsync(ct);

    public async Task<bool> ExistsByNameAsync(Guid userId, string name, Guid? excludeId = null, CancellationToken ct = default)
    {
        var normalizedName = name.Trim().ToLower();

        return await _context.Categories.AnyAsync(c =>
            c.UserId == userId
            && c.Name.ToLower() == normalizedName
            && (excludeId == null || c.Id != excludeId), ct);
    }

    public Task<bool> IsInUseAsync(Guid id, CancellationToken ct = default)
    {
        // TODO(Phase 4): true when any Transaction or Budget references this category id.
        throw new NotImplementedException();
    }

    public void Add(Category category) => _context.Categories.Add(category);

    public void Remove(Category category) => _context.Categories.Remove(category);
}
