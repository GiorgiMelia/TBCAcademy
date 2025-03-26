using Microsoft.AspNetCore.Identity;
using offers.itacademy.ge.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.Dtos
{
    public class CreateClientDto 
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }


    }
}
