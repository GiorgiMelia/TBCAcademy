using System;

namespace offers.itacademy.ge.Domain.entities
{
    public class Company
    {
        public int? CompanyId { get; set; }
        public bool IsActive { get; set; } = false;
        public string? PhotoUrl { get; set; }   
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        //    lists
    }
}
 