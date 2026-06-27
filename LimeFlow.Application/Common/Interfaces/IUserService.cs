using LimeFlow.Application.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Common.Interfaces
{
    public interface IUserService
    {
        Task<IReadOnlyList<UserResponseDto>> GetUsersService();
        Task<UserResponseDto> GetUserByIdService(Guid id);
        Task<UserResponseDto> CreateUserService(CreateUserRequestDto request);
        Task DeleteUserService(Guid id);


    }
}
