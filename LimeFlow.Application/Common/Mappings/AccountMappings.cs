using LimeFlow.Application.Common.DTOs;
using LimeFlow.Domain.Entities;

namespace LimeFlow.Application.Common.Mappings;

public static class AccountMappings
{
    public static AccountResponse ToResponse(this Account account) =>
        new(account.Id, account.Name, account.Bank, account.Balance, account.CreatedAt, account.UpdatedAt);
}
