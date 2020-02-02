using gRPC.Net.Terminal.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gRPC.Net.Terminal.Services
{
    public class CustomerPriceService
    {
        private readonly ProductPriceService _productPriceService = new ProductPriceService();

        public async Task<IList<CustomerPrice>> GetCustomersPrices()
        {
            var productPrices = await _productPriceService.GetProductBasePrices();

            var newCustomerPrices = new List<CustomerPrice>();

            foreach (var customerId in Customers.Ids)
            {
                foreach (var baseProductPrice in productPrices)
                {
                    var calculatedPrice = await CalculateCustomerPrice(customerId, baseProductPrice.Price);
                    var customerPrice = new CustomerPrice(customerId, baseProductPrice.ProductId, calculatedPrice);
                    newCustomerPrices.Add(customerPrice);
                }
            }

            return newCustomerPrices;
        }

        private async Task<double> CalculateCustomerPrice(int customerId, double newPrice)
        {
            await Task.Delay(50);
            return newPrice - (Customers.Promotion[customerId] * newPrice);
        }
    }
}
