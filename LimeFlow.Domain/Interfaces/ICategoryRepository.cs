using LimeFlow.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LimeFlow.Domain.Interfaces
{
    public interface ICategoryRepository
    {

        Task CreateAsync(Category category);
        Task UpdateAsync(Category category);
        Task DeleteAsync(Guid id);
        Task<Category> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Category>> GetAllAsync();
    }
}
