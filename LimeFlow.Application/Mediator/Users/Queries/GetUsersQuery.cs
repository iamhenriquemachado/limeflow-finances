using LimeFlow.Application.Common.DTOs;
using LimeFlow.Application.Common.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LimeFlow.Application.Mediator.Users.Queries
{

    public record GetUsersQuery() : IRequest<IReadOnlyList<UserResponseDto>>;
}
