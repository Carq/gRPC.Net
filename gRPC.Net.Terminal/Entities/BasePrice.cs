namespace gRPC.Net.Terminal.Entities
{
    public class BasePrice : BaseEntity
    {
        public BasePrice(double price, int productId)
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
