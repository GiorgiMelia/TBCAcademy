namespace ITAcademy.Offers.Domain.Entities
{
    using Microsoft.AspNetCore.Identity;

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
