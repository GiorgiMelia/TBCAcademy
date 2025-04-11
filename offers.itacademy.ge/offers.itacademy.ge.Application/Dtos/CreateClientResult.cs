using Microsoft.AspNetCore.Identity;
using offers.itacademy.ge.Domain.entities;

namespace offers.itacademy.ge.Application.Dtos
{
    public class CreateClientResult
    {
        public IdentityResult IdentityResult { get; set; }

        public Client Client { get; set; }
    }
}
