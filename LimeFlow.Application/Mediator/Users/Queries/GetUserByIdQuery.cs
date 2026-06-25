using LimeFlow.Application.Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Application.Mediator.Users.Queries
{

    public record GetUserByIdQuery(Guid Id) : IRequest<UserResponseDto>;
}
