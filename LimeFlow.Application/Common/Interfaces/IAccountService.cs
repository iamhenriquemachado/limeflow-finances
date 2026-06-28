using LimeFlow.Application.Common.DTOs;
using LimeFlow.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Common.Interfaces
{
    public interface IAccountService
    {
        Task<CreateAccountReponseDto> CreateAccountService(Account request);
    }
}
