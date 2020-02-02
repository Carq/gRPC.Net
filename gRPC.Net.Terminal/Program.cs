using gRPC.Net.Terminal.Entities;
using gRPC.Net.Terminal.Helpers;
using gRPC.Net.Terminal.Services;
using gRPC.Net.Terminal.Storage;
using System;
using System.Collections.Generic;
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

            DisplayMenu();

            var key = Console.ReadKey().Key;
            while (key != ConsoleKey.Escape)
            {
                Console.WriteLine();
                var task = key switch
                {
                    ConsoleKey.D1 => GetCustomerPrices(),
                    ConsoleKey.D5 => Task.Run(() => DisplayMenu()),
                    _ => Task.CompletedTask,
                };

                await task;
                key = Console.ReadKey().Key;
            }
        }

        private static async Task GetCustomerPrices()
        {
            var customerPrices = await _customerPriceService.GetCustomersPrices();

            DisplayCustomerPrices(customerPrices);

            Console.WriteLine();
        }

        private static void DisplayCustomerPrices(IList<CustomerPrice> customerPrices)
        {
            HappyConsole.WriteDarkGreenLine("CustomerId == ProductId == Price == Active");
            HappyConsole.WriteDarkGreenLine("==========================================");
            foreach (var customerPrice in customerPrices)
            {
                HappyConsole.WriteGreenLine($"{customerPrice.CustomerId,2} {customerPrice.ProductId,13} {customerPrice.Price.ToString("C2"),17} {(customerPrice.IsActive ? "A" : ""),3}");
            }

            HappyConsole.WriteDarkGreenLine("==========================================");
        }

        private static void DisplayMenu()
        {
            Console.Clear();
            HappyConsole.WriteGrayLine("=== Menu:");
            HappyConsole.WriteGrayLine("= 1 - GetCustomersPrices()");
            HappyConsole.WriteGrayLine("= 5 - Clear terminal");
            HappyConsole.WriteGrayLine("== Esc - Exit");
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
