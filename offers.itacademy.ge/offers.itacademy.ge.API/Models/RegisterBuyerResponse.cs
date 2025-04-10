namespace ITAcademy.Offers.API.Models
{
    public class RegisterBuyerResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; } = "Buyer";
    }

}
