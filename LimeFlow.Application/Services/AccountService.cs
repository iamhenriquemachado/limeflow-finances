using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Models;

namespace LimeFlow.Application.Services
{
    public class AccountService : IAccountService
    {

        private readonly IAccountRepository _repo;

        public AccountService(IAccountRepository repo)
        {
            _repo = repo;
        }
        public async Task<CreateAccountReponseDto> CreateAccountService(Account request)
        {
            await _repo.CreateAsync(request);

            var accountResponseDto = new CreateAccountReponseDto(request.Id, request.Name, request.Bank, request.CreatedAt);

            return accountResponseDto;
        }

        public async Task<IEnumerable<CreateAccountReponseDto>> GetAllAccountsService()
        {
            var accounts = await _repo.GetAllAsync();

            var accountResponseDto = accounts.Select(a => new CreateAccountReponseDto(a.Id, a.Name, a.Bank, a.CreatedAt)).ToList();

            return accountResponseDto;
        }

        public async Task<CreateAccountReponseDto> GetAccountByIdService(Guid id)
        {
            var account = await _repo.GetByIdAsync(id);

            var accountResponseDto = new CreateAccountReponseDto(account.Id, account.Name, account.Bank, account.CreatedAt);

            return accountResponseDto;
        }

        // falta implementar
        public async Task UpdateAccountService(UpdateAccountRequestDto request)
        {
            Account account = new Account();

            account.UpdateBank(request.Bank);
            account.UpdateName(request.Name);


        }

        public async Task DeleteAccountByIdService(Guid id)
        {
            var account = await _repo.GetByIdAsync(id);

            if (account == null)
            {
                throw new ArgumentNullException(nameof(id), "Account id invalid");
            }

            await _repo.DeleteAsync(id);

        }

    }
}
