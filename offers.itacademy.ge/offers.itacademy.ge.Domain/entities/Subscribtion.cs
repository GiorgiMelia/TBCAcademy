namespace offers.itacademy.ge.Domain.entities
{

    public class Subscription
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
