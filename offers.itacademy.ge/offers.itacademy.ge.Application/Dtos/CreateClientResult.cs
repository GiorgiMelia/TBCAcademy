using Microsoft.AspNetCore.Identity;
using offers.itacademy.ge.Domain.entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.Dtos
{
    public class CreateClientResult
    {
        public IdentityResult IdentityResult { get; set; }
        public Client Client {  get; set; }  
    }
}
