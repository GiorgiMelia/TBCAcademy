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
        public Buyer Buyer { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool IsCanceled { get; set; }

    }
}
