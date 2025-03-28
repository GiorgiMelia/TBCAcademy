namespace offers.itacademy.ge.Domain.entities
{
    public class Offer
    {
        public int Id { get; set; }

        public Product Product { get; set; }
        //productID

        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public double Price { get; set; }

        public bool IsArchived { get; set; }


    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


    }
    public class Purchase
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public Buyer Buyer { get; set; }
        public int ProductId { get; set; }
        public DateTime PurchaseDate { get; set; }

    }
    public class Subscription
    {
        public int Id { get; set; }
        public int BuyerId { get; set; }
        public Category Category { get; set; }
    }
    public class Category
    {
        public int Id { get; set; }
        public List<Product> Products { get; set; }

    }

}
