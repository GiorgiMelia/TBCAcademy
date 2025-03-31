using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.Dtos
{
    public class SubscriptionResponse
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }

}
