using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.DTOs
{

    public sealed record CreateUserRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public record UserResponseDto(Guid Id, string Email, string Name, DateTime CreatedAt, DateTime LastUpdatedAt);
}
