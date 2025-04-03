using offers.itacademy.ge.API.Attributes;
using System.ComponentModel.DataAnnotations;

namespace offers.itacademy.ge.API.Models
{
    public class OfferRequest
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name must be less than 100 characters.")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product description is required.")]
        [StringLength(500, ErrorMessage = "Product description must be less than 500 characters.")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "End date is required.")]
        [ValidEndDateAttribute("End date must be in the future.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "CategoryId is required.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

    }
}
