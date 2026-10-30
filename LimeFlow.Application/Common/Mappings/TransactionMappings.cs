using LimeFlow.Application.Common.DTOs;
using LimeFlow.Domain.Entities;

namespace LimeFlow.Application.Common.Mappings;

public static class TransactionMappings
{
    public static TransactionResponse ToResponse(this Transaction transaction) =>
        new(
            transaction.Id,
            transaction.AccountId,
            transaction.CategoryId,
            transaction.Type,
            transaction.Amount,
            transaction.Description,
            transaction.TransactionDate,
            transaction.CreatedAt);
}
