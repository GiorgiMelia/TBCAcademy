using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using offers.itacademy.ge.Application.Dtos;

namespace offers.itacademy.ge.API.Models
{
    public class PurchaseResponse
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public int BuyerId { get; set; }
        public string BuyerNameAndSurname { get; set; }

        public int Quantity { get; set; }
        public double TotalPayment { get; set; }
        public DateTime PurchaseDate { get; set; }
        //public bool IsCanceled { get; set; }
    }
}
