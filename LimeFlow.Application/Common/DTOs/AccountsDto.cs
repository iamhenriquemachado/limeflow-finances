using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Common.DTOs
{
    public record CreateAccountRequestDto(string Name, string Bank);

    public record AccountCreatedResponseDto(Guid Id, string Name, string Bank, DateTime CreatedAt);
    public record UpdateAccountRequestDto(Guid Id, string Name, string Bank, DateTime LastUpdatedAt);

    public record GetAccountRequestDto(int PageNumber, int PageSize);
    public record AccountSummaryResponseDto(Guid Id, string Name, string Bank, decimal Balance, DateTime CreatedAt);


}
