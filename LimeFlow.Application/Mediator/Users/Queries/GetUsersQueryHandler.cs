using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using MediatR;


namespace LimeFlow.Application.Mediator.Users.Queries
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, IReadOnlyList<UserResponseDto>>
    {
        private readonly IUserRepository _repo;

        public GetUsersQueryHandler(IUserRepository repo)
        {
            _repo = repo;
        }

        public async Task<IReadOnlyList<UserResponseDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _repo.GetAllAsync();

            var userResponseDto = users.Select(u => new UserResponseDto(
                u.Id, 
                u.Name, 
                u.Email, 
                u.CreatedAt
            )).ToList();

            return userResponseDto;
        }
    }
}
