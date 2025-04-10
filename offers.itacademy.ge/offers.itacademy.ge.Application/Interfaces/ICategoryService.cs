using ITAcademy.Offers.Application.Dtos;
using ITAcademy.Offers.Domain.Entities;

namespace ITAcademy.Offers.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> CreateCategory(CategoryDto categoryDto);
        Task<List<Category>> GetAllCategories();
        Task<Category?> GetCategoryById(int id);
        Task<bool> UpdateCategory(int id, CategoryDto dto);
        Task<bool> DeleteCategory(int id);
    }



}
