using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Common.DTOs
{
    public record UserDataResponseDto(Guid Id, string Email, string Password, string Name, DateTime CreatedAt);

    public record UserResponseDto(Guid Id, string Email, string Name, DateTime CreatedAt);
}
