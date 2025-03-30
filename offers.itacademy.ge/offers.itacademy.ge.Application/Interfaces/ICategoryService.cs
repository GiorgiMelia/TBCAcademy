using offers.itacademy.ge.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<Category> CreateCategoryAsync(string categoryName);
        Task<List<Category>> GetAllCategoriesAsync();
        Task<Category?> GetCategoryByIdAsync(int id);
    }



}
