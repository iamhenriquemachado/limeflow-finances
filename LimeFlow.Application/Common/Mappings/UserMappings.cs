using LimeFlow.Application.Common.DTOs;
using LimeFlow.Domain.Entities;

namespace LimeFlow.Application.Common.Mappings;

public static class UserMappings
{
    public static UserResponse ToResponse(this User user) =>
        new(user.Id, user.Name, user.Email, user.CreatedAt, user.UpdatedAt);
}
