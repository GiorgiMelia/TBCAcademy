namespace offers.itacademy.ge.API.Models
{
    public class RegisterCompanyResponse
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; } = "Company";
    }
}
