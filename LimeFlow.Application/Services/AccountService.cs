using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Models;

namespace LimeFlow.Application.Services
{
    public class AccountService : IAccountService
    {

        private readonly IAccountRepository _repo;

        public Task<AccountCreatedResponseDto> CreateAsync(CreateAccountRequestDto request)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<AccountSummaryResponseDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<AccountSummaryResponseDto> GetByAsyncId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(UpdateAccountRequestDto request)
        {
            throw new NotImplementedException();
        }
    }
}
