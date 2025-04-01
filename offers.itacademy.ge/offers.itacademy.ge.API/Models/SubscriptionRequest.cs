using offers.itacademy.ge.Domain.entities;

namespace offers.itacademy.ge.API.Models
{
    public class SubscriptionRequest
    {
        public int BuyerId { get; set; }
        public int CategoryId { get; set; }
    }
}
