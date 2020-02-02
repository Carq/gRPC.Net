namespace gRPC.Net.Terminal.Entities
{
    public class ProductPrice : BaseEntity
    {
        public ProductPrice(double price, int productId)
        {
            Price = price;
            ProductId = productId;
        }

        public double Price { get; private set; }

        public int ProductId { get; private set; }

        public void ChangePrice(double newPrice)
        {
            Price = newPrice;
        }
    }
}
