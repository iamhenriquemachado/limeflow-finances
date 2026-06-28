using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            
    }
}
