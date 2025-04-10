using ITAcademy.Offers.Application.Exceptions;
using ITAcademy.Offers.Application.Interfaces;
using ITAcademy.Offers.Domain.Entities;
using ITAcademy.Offers.Persistence.Data;
using Microsoft.EntityFrameworkCore;


namespace ITAcademy.Offers.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<bool> CategoryExists(string name)
        {
            return await _context.Categories.AnyAsync(o => o.Name == name);

        }

        public async Task<Category?> GetCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var hasOffers = await _context.Offers.AnyAsync(o => o.CategoryId == id);
            if (hasOffers)
                throw new WrongRequestException("Cannot delete category with associated offers.");

            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);

            if (category == null)
                return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
