
namespace ITAcademy.Offers.Application.Dtos
{
    public class OfferDto
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        public DateTime EndDate { get; set; }
        public int CategoryId { get; set; }
        public int CompanyId { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
