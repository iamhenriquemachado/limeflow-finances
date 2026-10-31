using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Common.Models;

namespace LimeFlow.Application.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICurrentUserService _currentUser;
    private readonly IUnitOfWork _unitOfWork;

    public TransactionService(
        ITransactionRepository transactionRepository,
        IAccountRepository accountRepository,
        ICategoryRepository categoryRepository,
        ICurrentUserService currentUser,
        IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _accountRepository = accountRepository;
        _categoryRepository = categoryRepository;
        _currentUser = currentUser;
        _unitOfWork = unitOfWork;
    }

    public Task<PagedResponse<TransactionResponse>> GetAllAsync(TransactionFilter filter, CancellationToken ct = default)
    {
        // TODO(Phase 5): call GetPagedAsync with filter fields + filter.Skip/Take, order by TransactionDate desc,
        // map items with ToResponse(), return new PagedResponse(items, filter.PageNumber, filter.Take, totalCount).
        throw new NotImplementedException();
    }

    public Task<TransactionResponse> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        // TODO(Phase 5): GetByIdAsync(id, userId) or throw NotFoundException(nameof(Transaction), id).
        throw new NotImplementedException();
    }

    public Task<TransactionResponse> CreateAsync(CreateTransactionRequest request, CancellationToken ct = default)
    {
        // TODO(Phase 5):
        // 1. Load the owned account (GetByIdAsync(request.AccountId, userId)) or throw NotFoundException. Do not commit.
        // 2. Load the owned category or throw NotFoundException. Category.Type must equal request.Type, otherwise DomainException.
        // 3. new Transaction(..., request.TransactionDate ?? DateTime.UtcNow).
        // 4. If Income: account.AddCredit(amount). If Expense: account.Debit(amount).
        // 5. _transactionRepository.Add(transaction); await _unitOfWork.CommitAsync(ct); return ToResponse().
        throw new NotImplementedException();
    }

    public Task<TransactionResponse> UpdateAsync(Guid id, UpdateTransactionRequest request, CancellationToken ct = default)
    {
        // TODO(Phase 5):
        // 1. Load the owned transaction or throw NotFoundException. Do not commit on any failure.
        // 2. Load the old account (transaction.AccountId) and revert the old effect
        //    (Income -> Debit, Expense -> AddCredit).
        // 3. Load the new owned account and category (404 if missing / not owned). Category.Type must match request.Type.
        // 4. transaction.Update(...); apply the new effect on the (possibly different) account.
        // 5. Single CommitAsync.
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        // TODO(Phase 5):
        // 1. Load the owned transaction and its account.
        // 2. Revert the effect (deleting a 50 Expense credits 50 back).
        // 3. Remove the transaction and CommitAsync once.
        throw new NotImplementedException();
    }
}
