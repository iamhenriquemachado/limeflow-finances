using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Common.DTOs
{
    public record LoginRequestDto(string email, string password);
    public record LoginResponseDto(string message, string token);
}
