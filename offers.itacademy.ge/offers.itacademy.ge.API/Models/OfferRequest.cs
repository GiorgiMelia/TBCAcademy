namespace offers.itacademy.ge.API.Models
{
    public class OfferRequest
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public DateTime EndDate { get; set; }
        public int CategoryId { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}
