using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Models;

namespace LimeFlow.Application.Services
{
    public class AccountService : IAccountService
    {

        private readonly IAccountRepository _repo;

        public async Task<AccountCreatedResponseDto> CreateAsync(CreateAccountRequestDto request)
        {

            Account account = new Account(request.Name, request.Bank);
            await _repo.CreateAsync(account);

            var accountCreatedResponseDto = new AccountCreatedResponseDto(account.Id, account.Name, account.Bank, account.CreatedAt);

            return accountCreatedResponseDto;

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
