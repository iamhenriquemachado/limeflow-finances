using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Services
{
    public class AuthService : IAuthService
    {
        public Task<LoginResponseDto?> LoginAsync(LoginRequestDto request, CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
