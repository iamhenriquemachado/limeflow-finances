using LimeFlow.Application.Common.Models;
using LimeFlow.Domain.Enums;

namespace LimeFlow.Application.Common.DTOs;

public record CreateTransactionRequest(
    Guid AccountId,
    Guid CategoryId,
    TransactionType Type,
    decimal Amount,
    string Description,
    DateTime? TransactionDate);

public record UpdateTransactionRequest(
    Guid AccountId,
    Guid CategoryId,
    TransactionType Type,
    decimal Amount,
    string Description,
    DateTime? TransactionDate);

public record TransactionResponse(
    Guid Id,
    Guid AccountId,
    Guid CategoryId,
    TransactionType Type,
    decimal Amount,
    string Description,
    DateTime TransactionDate,
    DateTime CreatedAt);

public record TransactionFilter : PagedRequest
{
    public Guid? AccountId { get; init; }
    public Guid? CategoryId { get; init; }
    public TransactionType? Type { get; init; }
    public DateTime? From { get; init; }
    public DateTime? To { get; init; }
}
