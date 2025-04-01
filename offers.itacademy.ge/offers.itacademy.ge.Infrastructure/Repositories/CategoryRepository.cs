using Microsoft.EntityFrameworkCore;
using offers.itacademy.ge.Application.Interfaces;
using offers.itacademy.ge.Domain.entities;
using offers.itacademy.ge.Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Infrastructure.Repositories
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

        public async Task<Category?> GetCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}
