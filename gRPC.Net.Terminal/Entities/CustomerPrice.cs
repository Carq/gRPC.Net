using System;

namespace gRPC.Net.Terminal.Entities
{
    public class CustomerPrice : BaseEntity
    {
        public CustomerPrice(int customerId, int productId, double price)
        {
            CustomerId = customerId;
            ProductId = productId;
            Price = price;
        }

        public int ProductId { get; private set; }

        public int CustomerId { get; private set; }

        public double Price { get; private set; }
    }
}
