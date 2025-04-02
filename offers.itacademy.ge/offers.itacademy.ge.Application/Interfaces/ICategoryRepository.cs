using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Domain.entities;

namespace offers.itacademy.ge.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategory(Category category);
        Task<List<Category>> GetAllCategories();
        Task<Category?> GetCategoryById(int id);
        Task UpdateCategory(Category category);
        Task<bool> DeleteCategory(int id);
    }
}
