using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Common.DTOs
{

    public record CreateAccountRequestDto(string Name, string Bank);

    public record CreateAccountReponseDto(Guid id, string Name, string Bank, DateTime CreatedAt);
    public record UpdateAccountRequestDto(string Name, string Bank, DateTime LastUpdatedAt);

    public record UpdateAccountResponseDto(string Name, string Bank, DateTime LastUpdatedAt);
}
