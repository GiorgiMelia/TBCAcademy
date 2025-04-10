namespace ITAcademy.Offers.API.Controllers
{
    using ITAcademy.Offers.API.Models;
    using ITAcademy.Offers.Application.Dtos;
    using ITAcademy.Offers.Application.Interfaces;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Policy = "MustAdmin")]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }


        [HttpPost]

        public async Task<ActionResult<CategoryResponse>> Create([FromBody] CategoryRequest request)
        {
            var category = await _categoryService.CreateCategory(new CategoryDto { CategoryName = request.CategoryName });
            var response = new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            };

            return CreatedAtAction(nameof(GetById), new { id = category.Id }, response);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAll()
        {
            var categories = await _categoryService.GetAllCategories();

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
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
                return NotFound();

            return Ok(new CategoryResponse
            {
                Id = category.Id,
                Name = category.Name
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CategoryDto dto)
        {
            var result = await _categoryService.UpdateCategory(id, dto);
            if (!result)
                return NotFound("Category not found.");

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteCategory(id);
            if (!result)
                return NotFound("Category not found.");

            return NoContent();
        }
    }
}