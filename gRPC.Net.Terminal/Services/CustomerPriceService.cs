using gRPC.Net.Terminal.Entities;
using gRPC.Net.Terminal.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gRPC.Net.Terminal.Services
{
    public class CustomerPriceService
    {
        private readonly PriceContext _priceContext = new PriceContext();

        private readonly ILogger _logger = LoggerFactory.Create(x => x.AddConsole()).CreateLogger<CustomerPriceService>();

        public async Task ChangeCustomerPricesForSpecificProduct(int productId, double newPrice)
        {
            var newCustomerPrices = new List<CustomerPrice>();
            foreach (var customerId in Customers.Ids)
            {
                var customerPrice = newPrice - (Customers.Promotion[customerId] * newPrice);
                _logger.LogInformation($"Customer {customerId} has new price {customerPrice.ToString("C2")} for Product {productId}");
                newCustomerPrices.Add(new CustomerPrice(customerId, productId, customerPrice, DateTime.Now));
            }

            await _priceContext.AddRangeAsync(newCustomerPrices);
        }
    }
}
