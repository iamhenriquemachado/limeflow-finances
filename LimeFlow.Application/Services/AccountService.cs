using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
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
        async Task<CreateAccountReponseDto> CreateAccountService(CreateAccountRequestDto request)
        {

            await _repo.CreateAsync();
        }

            
    }
}
