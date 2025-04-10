using System.ComponentModel.DataAnnotations;

namespace ITAcademy.Offers.API.Models
{
    public class CategoryRequest
    {
        [Required(ErrorMessage = "Category name is required.")]
        public string CategoryName { get; set; }

    }
}
