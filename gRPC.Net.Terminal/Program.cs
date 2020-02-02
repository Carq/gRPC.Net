using gRPC.Net.Terminal.Entities;
using gRPC.Net.Terminal.Helpers;
using gRPC.Net.Terminal.Services;
using gRPC.Net.Terminal.Storage;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace gRPC.Net.Terminal
{
    class Program
    {
        private static readonly CustomerPriceService _customerPriceService = new CustomerPriceService();

        private static readonly PriceContext _priceContext = new PriceContext();

        static async Task Main(string[] args)
        {
            await InitializeDatabase();

            var customerPrices = await _customerPriceService.GetCustomersPrices();

            HappyConsole.WriteDarkGreenLine("CustomerId == ProductId == Price");
            HappyConsole.WriteDarkGreenLine("================================");
            foreach (var customerPrice in customerPrices)
            {
                HappyConsole.WriteGreenLine($"{customerPrice.CustomerId} {customerPrice.ProductId, 13} {customerPrice.Price.ToString("C2"), 17}");
            }

            HappyConsole.WriteDarkGreenLine("================================");

            Console.ReadKey();
        }

        static async Task InitializeDatabase()
        {
            await _priceContext.Database.EnsureCreatedAsync();

            if (_priceContext.ProductPrices.Any())
            {
                return;
            }

            for (int i = 0; i <= 6; i++)
            {
                _priceContext.ProductPrices.Add(new ProductPrice(i * 100, i + 10));
            }

            await _priceContext.SaveChangesAsync();
        }
    }
}
