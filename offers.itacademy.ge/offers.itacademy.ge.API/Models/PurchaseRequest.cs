using System.ComponentModel.DataAnnotations;

namespace ITAcademy.Offers.API.Models
{
    public class PurchaseRequest
    {
        [Required(ErrorMessage = "OfferId is required.")]
        public int OfferId { get; set; }


        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}