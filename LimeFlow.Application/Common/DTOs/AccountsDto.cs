using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Common.DTOs
{
    public record UpdateAccountRequestsDto(string Name, string Bank);

    public record UpdateAccountResponse(Guid Id, string Name, string Bank, DateTime CreteadAt);
}
