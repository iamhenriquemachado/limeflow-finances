using LimeFlow.Application.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<LoginResposeDto?> LoginAsync(LoginRequestDto request, CancellationToken ct);
    }
}
