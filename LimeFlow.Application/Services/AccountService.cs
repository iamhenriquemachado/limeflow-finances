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

            Account account = new Account(request.Id, request.Name, request.Bank);
            await _repo.CreateAsync(account);

            var accountCreatedResponseDto = new AccountCreatedResponseDto(account.Id, account.Name, account.Bank, account.CreatedAt);

            return accountCreatedResponseDto;

        }

        public async Task DeleteAsync(Guid id)
        {
            var account = await _repo.GetByIdAsync(id);

            if (account != null)
                await _repo.DeleteAsync(id);
        }

        public async Task<IReadOnlyList<AccountSummaryResponseDto>> GetAllAsync(Guid userId)
        {
            var accounts = await _repo.GetAllAsync(userId);

            if (accounts != null)
            {
                var accountResponseDto = accounts.Select(a => new AccountSummaryResponseDto(a.Id, a.Name, a.Bank, a.Balance, a.CreatedAt)).ToList();

                return accountResponseDto;
            }

            return null;
        }

        public async Task<AccountSummaryResponseDto> GetByIdAsync(Guid id)
        {
            var account = await _repo.GetByIdAsync(id);

            if (account != null)
            {
                var accountResponseDto = new AccountSummaryResponseDto(account.Id, account.Name, account.Bank, account.Balance, account.CreatedAt);
                return accountResponseDto;

            }

            return null;
        }

        public async Task UpdateAsync(UpdateAccountRequestDto request)
        {
            var account = await _repo.GetByIdAsync(request.Id);

            account.UpdateBank(request.Bank);
            account.UpdateName(request.Name);

            if (account != null)
            {
                await _repo.UpdateAsync(account);

            }


        }
    }
}
