using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace offers.itacademy.ge.Domain.entities
{
    public class Purchase
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int BuyerId { get; set; }
        public Buyer Buyer { get; set; }
        public int OfferId { get; set; }
        public Offer Offer { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool IsCanceled { get; set; }

    }
}
