namespace offers.itacademy.ge.API.Models
{
    public class CreateOfferRequest
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public double Price { get; set; }
        public int Quantity { get; set; }

    }
}
