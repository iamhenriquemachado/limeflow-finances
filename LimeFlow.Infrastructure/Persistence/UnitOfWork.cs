using LimeFlow.Application.Common.Interfaces;

namespace LimeFlow.Infrastructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
    }

    public async Task<bool> CommitAsync(CancellationToken ct = default) =>
        await _context.SaveChangesAsync(ct) > 0;
}
