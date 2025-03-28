namespace offers.itacademy.ge.Domain.entities
{
    using Microsoft.AspNetCore.Identity;
    using offers.itacademy.ge.Domain.entities;

    public class Client : IdentityUser
    {

        public UserType UserType { get; set; }

        public int? CompanyId { get; set; }

        public virtual Company? Company { get; set; }

        public int? BuyerId { get; set; }

        public virtual Buyer? Buyer { get; set; }
    }

    public enum UserType
    {
        Company,
        Buyer
    }
}
