using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Entities;
using LimeFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LimeFlow.Infrastructure.Repositories;

public class GoalRepository : IGoalRepository
{
    private readonly AppDbContext _context;

    public GoalRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Goal?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct = default) =>
        _context.Goals.FirstOrDefaultAsync(g => g.Id == id && g.UserId == userId, ct);

    public async Task<IReadOnlyList<Goal>> GetAllAsync(Guid userId, CancellationToken ct = default) =>
        await _context.Goals
            .AsNoTracking()
            .Where(g => g.UserId == userId)
            .OrderBy(g => g.TargetDate)
            .ToListAsync(ct);

    public void Add(Goal goal) => _context.Goals.Add(goal);

    public void Remove(Goal goal) => _context.Goals.Remove(goal);
}
