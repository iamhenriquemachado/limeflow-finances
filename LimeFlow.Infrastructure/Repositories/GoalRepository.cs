using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Entities;
using LimeFlow.Infrastructure.Persistence;

namespace LimeFlow.Infrastructure.Repositories;

public class GoalRepository : IGoalRepository
{
    private readonly AppDbContext _context;

    public GoalRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<Goal?> GetByIdAsync(Guid id, Guid userId, CancellationToken ct = default)
    {
        // TODO(Phase 7): FirstOrDefaultAsync where Id == id && UserId == userId.
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Goal>> GetAllAsync(Guid userId, CancellationToken ct = default)
    {
        // TODO(Phase 7): AsNoTracking, filter by userId, OrderBy TargetDate.
        throw new NotImplementedException();
    }

    public void Add(Goal goal) => _context.Goals.Add(goal);

    public void Remove(Goal goal) => _context.Goals.Remove(goal);
}
