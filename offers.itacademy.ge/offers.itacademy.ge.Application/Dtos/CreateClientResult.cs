using ITAcademy.Offers.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace ITAcademy.Offers.Application.Dtos
{
    public class CreateClientResult
    {
        public IdentityResult IdentityResult { get; set; }

        public Client Client { get; set; }
    }
}
