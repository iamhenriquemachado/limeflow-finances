using LimeFlow.Application.Common.Exceptions;
using LimeFlow.Application.Common.Interfaces;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace LimeFlow.API.Infrastructure;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid UserId
    {
        get
        {
            var principal = _httpContextAccessor.HttpContext?.User;
            var value = principal?.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? principal?.FindFirstValue(JwtRegisteredClaimNames.Sub);

            return Guid.TryParse(value, out var userId)
                ? userId
                : throw new UnauthorizedException("User is not authenticated.");
        }
    }
}
