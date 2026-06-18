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

        Task CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(int id);
        Task DeleteCategoryAsync(int id);
        Task<Category> GetCategoryAsync(int id);
        Task<IReadOnlyList<Category>> GetAllCategoryAsync();
    }
}
