using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.Dtos
{
    public class RegisterCompanyDto
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string? PhotoUrl { get; set; }
    }

}
