namespace ITAcademy.Offers.Domain.Entities
{
    public class Offer
    {
        public int Id { get; set; }

        public string ProductName { get; set; }
        public string ProductDescription { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public int? CompanyId { get; set; }

        public Company Company { get; set; } = null!;

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsArchived { get; set; }
        public bool IsCanceled { get; set; }


    }



}
