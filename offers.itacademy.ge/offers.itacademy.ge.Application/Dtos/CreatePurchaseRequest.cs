using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Application.Dtos
{
    public class CreatePurchaseRequest
    {
        public int OfferId { get; set; }
        public int BuyerId { get; set; }
        public int Quantity { get; set; }
    }

}
