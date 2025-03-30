namespace offers.itacademy.ge.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using offers.itacademy.ge.API.Models;
    using offers.itacademy.ge.Application.Interfaces;
    using System;

    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpPost]
        public async Task<ActionResult<CategoryResponse>> Create([FromBody] CreateCategoryRequest request)
        {
            var category = await _categoryService.CreateCategoryAsync(request.CategoryName);
            var response = new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            };

            return CreatedAtAction(nameof(GetById), new { id = category.Id }, response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAll()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();

            var response = categories.Select(c => new CategoryResponse
            {
                Id = c.Id,
                Name = c.Name
            });

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetById(int id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            });
        }
    }
}



