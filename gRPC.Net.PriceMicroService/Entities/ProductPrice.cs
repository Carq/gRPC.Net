namespace gRPC.Net.PriceMicroService.Entities
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

        public bool IsActive { get; set; }
    }
}
