using LimeFlow.Application.Common.DTOs;
using LimeFlow.Domain.Models;


namespace LimeFlow.Application.Common.Interfaces
{
    public interface IAccountService
    {
        Task<CreateAccountReponseDto> CreateAccountService(Account request);

        Task<IEnumerable<CreateAccountReponseDto>> GetAllAccountsService();

        Task<CreateAccountReponseDto> GetAccountByIdService(Guid id);
    }
}
