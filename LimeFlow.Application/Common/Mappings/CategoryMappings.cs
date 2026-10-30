using LimeFlow.Application.Common.DTOs;
using LimeFlow.Domain.Entities;

namespace LimeFlow.Application.Common.Mappings;

public static class CategoryMappings
{
    public static CategoryResponse ToResponse(this Category category) =>
        new(category.Id, category.Name, category.Type, category.CreatedAt);
}
