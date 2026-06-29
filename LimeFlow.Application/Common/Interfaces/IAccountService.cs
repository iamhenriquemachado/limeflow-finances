using LimeFlow.Application.Common.DTOs;
using LimeFlow.Domain.Models;


namespace LimeFlow.Application.Common.Interfaces
{
    public interface IAccountService
    {
        Task<AccountCreatedResponseDto> CreateAsync(CreateAccountRequestDto request);
        Task DeleteAsync(Guid id);
        Task<AccountSummaryResponseDto> GetByAsyncId(Guid id);
        Task<IReadOnlyList<AccountSummaryResponseDto>> GetAllAsync();
        Task UpdateAsync(UpdateAccountRequestDto request);
    }
}
