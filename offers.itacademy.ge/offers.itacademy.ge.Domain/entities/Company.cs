namespace offers.itacademy.ge.Domain.entities
{
    public class Company
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; } = false;

        public string? PhotoUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // lists
    }
}
