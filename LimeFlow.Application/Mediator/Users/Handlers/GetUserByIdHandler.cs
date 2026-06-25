using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using LimeFlow.Application.Mediator.Users.Queries;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Mediator.Users.Handlers
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserResponseDto>
    {
        private readonly IUserRepository _repo;

        public GetUserByIdQueryHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<UserResponseDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _repo.GetByIdAsync(request.Id);
            var userResponseDto = new UserResponseDto(user.Id, user.Name, user.Email, user.CreatedAt);

            return userResponseDto;
        }
    }
}
