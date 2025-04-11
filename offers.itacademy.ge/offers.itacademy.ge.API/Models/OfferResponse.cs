using offers.itacademy.ge.Domain.entities;

namespace offers.itacademy.ge.API.Models
{
    public class OfferResponse
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int CategoryId { get; set; }
        public int? CompanyId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }
        public bool IsArchived { get; set; }
        public bool IsCanceled{ get; set; }
        public int Quantity { get; set; }

    }
}
