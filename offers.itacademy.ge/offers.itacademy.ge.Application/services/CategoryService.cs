using ITAcademy.Offers.Application.Dtos;
using ITAcademy.Offers.Application.Exceptions;
using ITAcademy.Offers.Application.Interfaces;
using ITAcademy.Offers.Domain.Entities;


namespace ITAcademy.Offers.Application.services
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

            if (await _categoryRepository.CategoryExists(categoryDto.CategoryName)) throw new WrongRequestException("Category already exists");
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
