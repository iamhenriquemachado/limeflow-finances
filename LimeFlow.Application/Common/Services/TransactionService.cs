using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Exceptions;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Common.Mappings;
using LimeFlow.Application.Common.Models;
using LimeFlow.Domain.Entities;
using LimeFlow.Domain.Enums;
using LimeFlow.Domain.Exceptions;

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

    public async Task<PagedResponse<TransactionResponse>> GetAllAsync(TransactionFilter filter, CancellationToken ct = default)
    {
        var (items, totalCount) = await _transactionRepository.GetPagedAsync(
            _currentUser.UserId,
            filter.AccountId,
            filter.CategoryId,
            filter.Type,
            filter.From,
            filter.To,
            filter.Skip,
            filter.Take,
            ct);

        var responses = items.Select(t => t.ToResponse()).ToList();

        return new PagedResponse<TransactionResponse>(responses, filter.PageNumber, filter.Take, totalCount);
    }

    public async Task<TransactionResponse> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id, _currentUser.UserId, ct) ??
            throw new NotFoundException(nameof(Transaction), id);

        return transaction.ToResponse();
    }

    public async Task<TransactionResponse> CreateAsync(CreateTransactionRequest request, CancellationToken ct = default)
    {
        var account = await _accountRepository.GetByIdAsync(request.AccountId, _currentUser.UserId, ct) ??
            throw new NotFoundException(nameof(Account), request.AccountId);

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, _currentUser.UserId, ct) ??
            throw new NotFoundException(nameof(Category), request.CategoryId);

        if (category.Type != request.Type)
            throw new DomainException("Category type must match the transaction type.");

        var transaction = new Transaction(request.AccountId, request.CategoryId, request.Type, request.Amount, request.Description, request.TransactionDate ?? DateTime.UtcNow);

        if (transaction.Type == TransactionType.Income)
            account.AddCredit(request.Amount);
        else 
            account.Debit(request.Amount);

        _transactionRepository.Add(transaction);
        await _unitOfWork.CommitAsync(ct);
        return transaction.ToResponse();
    }

    public async Task<TransactionResponse> UpdateAsync(Guid id, UpdateTransactionRequest request, CancellationToken ct = default)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id, _currentUser.UserId, ct)
            ?? throw new NotFoundException(nameof(Transaction), id);

        var account = await _accountRepository.GetByIdAsync(request.AccountId, _currentUser.UserId, ct)
            ?? throw new NotFoundException(nameof(Account), request.AccountId);

        var category = await _categoryRepository.GetByIdAsync(request.CategoryId, _currentUser.UserId, ct)
            ?? throw new NotFoundException(nameof(Category), request.CategoryId);

        if (category.Type != request.Type)
            throw new DomainException("Category type must match the transaction type.");

        var oldAccount = await _accountRepository.GetByIdAsync(transaction.AccountId, _currentUser.UserId, ct)
            ?? throw new NotFoundException(nameof(Account), transaction.AccountId);

        if (transaction.Type == TransactionType.Income)
            oldAccount.Debit(transaction.Amount);
        else
            oldAccount.AddCredit(transaction.Amount);

        transaction.Update(
            request.AccountId,
            request.CategoryId,
            request.Type,
            request.Amount,
            request.Description,
            request.TransactionDate ?? DateTime.UtcNow);

        if (transaction.Type == TransactionType.Income)
            account.AddCredit(transaction.Amount);
        else
            account.Debit(transaction.Amount);

        await _unitOfWork.CommitAsync(ct);
        return transaction.ToResponse();
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct = default)
    {
        var transaction = await _transactionRepository.GetByIdAsync(id, _currentUser.UserId, ct)
            ?? throw new NotFoundException(nameof(Transaction), id);

        var account = await _accountRepository.GetByIdAsync(transaction.AccountId, _currentUser.UserId, ct)
            ?? throw new NotFoundException(nameof(Account), transaction.AccountId);

        if (transaction.Type == TransactionType.Income)
            account.Debit(transaction.Amount);
        else
            account.AddCredit(transaction.Amount);

        _transactionRepository.Remove(transaction);
        await _unitOfWork.CommitAsync(ct);
    }
}
