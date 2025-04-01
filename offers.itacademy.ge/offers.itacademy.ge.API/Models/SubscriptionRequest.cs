using offers.itacademy.ge.Domain.entities;
using System.ComponentModel.DataAnnotations;

namespace offers.itacademy.ge.API.Models
{
    public class SubscriptionRequest
    {
        [Required(ErrorMessage = "BuyerId is required.")]
        public int BuyerId { get; set; }

        [Required(ErrorMessage = "CategoryId is required.")]
        public int CategoryId { get; set; }
    }
}
