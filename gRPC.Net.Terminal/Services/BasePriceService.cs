using gRPC.Net.Terminal.Entities;
using gRPC.Net.Terminal.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace gRPC.Net.Terminal.Services
{
    public class BasePriceService
    {
        private readonly PriceContext _priceContext = new PriceContext();

        private readonly ILogger _logger = LoggerFactory.Create(x => x.AddConsole()).CreateLogger<BasePriceService>();

        private readonly CustomerPriceService _customerService = new CustomerPriceService();

        public async Task ChangeBasePrice(int productId, double newPrice)
        {
            var basePrice = await GetBasePrice(productId);
            
            basePrice.ChangePrice(newPrice);
            _logger.LogInformation($"Product {productId} has new Price {newPrice.ToString("C2")}");

            await _priceContext.SaveChangesAsync();

            await _customerService.ChangeCustomerPricesForSpecificProduct(productId, newPrice);
        }

        private async Task<BasePrice> GetBasePrice(int productId)
        {
            var basePrice = await _priceContext.BasePrices.SingleOrDefaultAsync(x => x.ProductId == productId);
            if (basePrice == null)
            {
                var newBasePrice = new BasePrice(0, productId);
                _priceContext.BasePrices.Add(newBasePrice);
                return newBasePrice;
            }

            return basePrice;
        }
    }
}
