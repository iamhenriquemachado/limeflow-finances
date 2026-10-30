using LimeFlow.Application.Common.Models;
using LimeFlow.Domain.Entities;

namespace LimeFlow.Application.Common.Interfaces;

public interface ITokenService
{
    AccessToken GenerateToken(User user);
}
