using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.Application.Dtos;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Domain.entities;


namespace offers.itacademy.ge.Application.services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> CreateCategory(CategoryDto categoryDto)
        {
            var category = new Category { Name = categoryDto.CategoryName };

            await _categoryRepository.CreateCategory(category);

            return category;
        }
        
        public async Task<List<Category>> GetAllCategories()
        {
            return await _categoryRepository.GetAllCategories();
        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await _categoryRepository.GetCategoryById(id);
        }
        public async Task<bool> UpdateCategory(int id, CategoryDto dto)
        {
            var category = await _categoryRepository.GetCategoryById(id);
            if (category == null)
                return false;

            category.Name = dto.CategoryName;
            await _categoryRepository.UpdateCategory(category);
            return true;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            return await _categoryRepository.DeleteCategory(id);
        }
    }
}
