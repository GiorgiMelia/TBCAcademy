namespace ITAcademy.Offers.Domain.Entities
{

    public class Subscription
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public Buyer Buyer { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
