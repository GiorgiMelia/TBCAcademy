namespace ITAcademy.Offers.Domain.Entities
{
    public class Buyer
    {

        public int? Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Address { get; set; }

        public decimal Balance { get; set; } = 0m;

        public string? PhotoUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
