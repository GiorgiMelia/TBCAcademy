using ITAcademy.Offers.Domain.Entities;

namespace ITAcademy.Offers.Application.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategory(Category category);
        Task<List<Category>> GetAllCategories();
        Task<Category?> GetCategoryById(int id);
        Task UpdateCategory(Category category);
        Task<bool> DeleteCategory(int id);
        Task<bool> CategoryExists(string name);
    }
}
