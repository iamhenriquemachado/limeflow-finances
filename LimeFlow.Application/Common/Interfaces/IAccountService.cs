using LimeFlow.Application.Common.DTOs;
using LimeFlow.Domain.Models;


namespace LimeFlow.Application.Common.Interfaces
{
    public interface IAccountService
    {
        Task<AccountCreatedResponseDto> CreateAsync(CreateAccountRequestDto request);
        Task DeleteAsync(Guid id);
        Task<AccountSummaryResponseDto> GetByIdAsync(Guid id);
        Task<IReadOnlyList<AccountSummaryResponseDto>> GetAllAsync(Guid userId);
        Task UpdateAsync(UpdateAccountRequestDto request);
    }
}
