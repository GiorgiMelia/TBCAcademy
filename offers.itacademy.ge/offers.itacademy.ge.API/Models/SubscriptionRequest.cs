using offers.itacademy.ge.Domain.entities;
using System.ComponentModel.DataAnnotations;

namespace offers.itacademy.ge.API.Models
{
    public class SubscriptionRequest
    {

        [Required(ErrorMessage = "CategoryId is required.")]
        public int CategoryId { get; set; }
    }
}
