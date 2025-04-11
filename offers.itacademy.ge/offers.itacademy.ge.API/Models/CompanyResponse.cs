namespace offers.itacademy.ge.API.Models
{
    public class CompanyResponse
    {
        public int? Id { get; set; }

        public string Name { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }
    }

}
