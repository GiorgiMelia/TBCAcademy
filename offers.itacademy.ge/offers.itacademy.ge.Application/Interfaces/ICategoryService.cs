using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Domain.entities;

namespace offers.itacademy.ge.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> CreateCategory(CategoryDto categoryDto);
        Task<List<Category>> GetAllCategories();
        Task<Category?> GetCategoryById(int id);
    }



}
