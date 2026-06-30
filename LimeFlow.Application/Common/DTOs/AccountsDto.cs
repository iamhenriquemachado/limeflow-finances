using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Common.DTOs
{

    //CREATE
    public record CreateAccountRequestDto(string Id, string Name, string Bank);

    public record AccountCreatedResponseDto(Guid id, string Name, string Bank, DateTime CreatedAt);

    // UPDATE
    public record UpdateAccountRequestDto(Guid Id, string Name, string Bank, DateTime LastUpdatedAt);

    // GET
    public record GetAccountRequestDto(int PageNumber, int PageSize);
    public record AccountSummaryResponseDto(Guid Id, string Name, string Bank, decimal Balance, DateTime CreatedAt);


}
