using offers.itacademy.ge.Domain.entities;

namespace offers.itacademy.ge.API.Models
{
    public class PurchaseRequest
    {
        public int OfferId { get; set; }
        public int BuyerId { get; set; }
        public int Quantity { get; set; }
    }
}
