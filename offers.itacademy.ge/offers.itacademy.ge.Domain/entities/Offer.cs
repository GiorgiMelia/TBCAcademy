namespace offers.itacademy.ge.Domain.entities
{
    public class Offer
    {
        public int Id { get; set; }

        public Product Product { get; set; }
        //productID

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double Price { get; set; }
        public bool IsArchived { get; set; }


    }

   

}
