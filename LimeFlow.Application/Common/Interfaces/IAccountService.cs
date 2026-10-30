using LimeFlow.Application.Common.DTOs;


namespace LimeFlow.Application.Common.Interfaces
{
    public interface IAccountService
    {
        Task<AccountCreatedResponseDto> CreateAsync(CreateAccountRequestDto request, Guid id);
        Task DeleteAsync(Guid id);
        Task<AccountSummaryResponseDto> GetByIdAsync(Guid id);
        Task<IReadOnlyList<AccountSummaryResponseDto>> GetAllAsync(Guid userId);
        Task UpdateAsync(UpdateAccountRequestDto request);
    }
}
