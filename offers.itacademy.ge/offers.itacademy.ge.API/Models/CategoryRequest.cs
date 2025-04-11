using System.ComponentModel.DataAnnotations;

namespace offers.itacademy.ge.API.Models
{
    public class CategoryRequest
    {
        [Required(ErrorMessage = "Category name is required.")]
        public string CategoryName { get; set; }

    }
}
