using LimeFlow.Application.Common.DTOs;


namespace LimeFlow.Application.Common.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(Guid userId, string name, string email);
    }
}
