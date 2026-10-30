namespace LimeFlow.Application.Common.Interfaces;

public interface IUnitOfWork
{
    Task<bool> CommitAsync(CancellationToken ct = default);
}
