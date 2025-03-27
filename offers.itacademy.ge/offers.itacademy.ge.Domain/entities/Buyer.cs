namespace offers.itacademy.ge.Domain.entities
{
    public class Buyer
    {
        public int? BuyerId { get; set; }
        public decimal Balance { get; set; } = 0m;
        public string? PhotoUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //lists
    }
}
