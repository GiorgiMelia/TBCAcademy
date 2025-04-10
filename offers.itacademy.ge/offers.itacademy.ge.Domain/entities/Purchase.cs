namespace ITAcademy.Offers.Domain.Entities
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
