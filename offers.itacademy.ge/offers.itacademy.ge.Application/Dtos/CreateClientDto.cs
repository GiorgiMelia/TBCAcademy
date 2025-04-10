using ITAcademy.Offers.Domain.Entities;

namespace ITAcademy.Offers.Application.Dtos
{
    public class CreateClientDto
    {
        public string Password { get; set; }

        public string Email { get; set; }

        public UserType UserType { get; set; }

        public Buyer? Buyer { get; set; }

        public Company? Company { get; set; }
    }
}
