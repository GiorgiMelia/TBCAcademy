using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Models
{
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
