using System.ComponentModel.DataAnnotations;

namespace ITAcademy.Offers.API.Models
{
    public class SubscriptionRequest
    {

        [Required(ErrorMessage = "CategoryId is required.")]
        public int CategoryId { get; set; }
    }
}
