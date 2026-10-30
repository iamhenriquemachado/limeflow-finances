using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Exceptions;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Common.Mappings;
using LimeFlow.Domain.Entities;

namespace LimeFlow.Application.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public AccountService(IAccountRepository accountRepository, ICurrentUserService currentUser, IUnitOfWork unitOfWork)
    {
        _accountRepository = accountRepository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<AccountResponse>> GetAllAsync(CancellationToken ct = default)
    {
        var accounts = await _accountRepository.GetAllAsync(_currentUser.UserId, ct);
        return accounts.Select(a => a.ToResponse()).ToList();
    }

    public async Task<AccountResponse> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var account = await GetOwnedAccountAsync(id, ct);
        return account.ToResponse();
    }

    public async Task<AccountResponse> CreateAsync(CreateAccountRequest request, CancellationToken ct = default)
    {
        var account = new Account(request.Name, request.Bank, _currentUser.UserId);

        _accountRepository.Add(account);
        await _unitOfWork.CommitAsync(ct);

        return account.ToResponse();
    }

    public async Task<AccountResponse> UpdateAsync(Guid id, UpdateAccountRequest request, CancellationToken ct = default)
    {
        var account = await GetOwnedAccountAsync(id, ct);

        account.Rename(request.Name, request.Bank);
        await _unitOfWork.CommitAsync(ct);

        return account.ToResponse();
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var account = await GetOwnedAccountAsync(id, ct);

        // TODO(Phase 3): decide the rule for accounts that have transactions
        // (throw ConflictException, or rely on the cascade delete) and document it in the README.
        _accountRepository.Remove(account);
        await _unitOfWork.CommitAsync(ct);
    }

    private async Task<Account> GetOwnedAccountAsync(Guid id, CancellationToken ct) =>
        await _accountRepository.GetByIdAsync(id, _currentUser.UserId, ct)
        ?? throw new NotFoundException(nameof(Account), id);
}
