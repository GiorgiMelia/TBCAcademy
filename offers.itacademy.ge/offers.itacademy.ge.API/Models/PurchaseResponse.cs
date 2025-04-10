namespace ITAcademy.Offers.API.Models
{
    public class PurchaseResponse
    {
        public int Id { get; set; }
        public int OfferId { get; set; }
        public int BuyerId { get; set; }

        public int Quantity { get; set; }
        public decimal TotalPayment { get; set; }
        public DateTime PurchaseDate { get; set; }
        public bool IsCanceled { get; set; }
    }
}
