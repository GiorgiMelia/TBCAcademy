using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using offers.itacademy.ge.Application.Dtos;

namespace offers.itacademy.ge.API.Models
{
    public class SubscriptionResponse
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public string BuyerNameAndSurname { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }

}
