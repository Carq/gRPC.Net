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
                    ConsoleKey.D2 => GetCustomerPricesRx(),
                    ConsoleKey.D3 => GetProductBasePrice(),
                    ConsoleKey.D4 => GetProductBasePricesRx(),
                    ConsoleKey.D5 => Task.Run(() => DisplayMenu()),
                    _ => Task.CompletedTask,
                };

                await task;
                key = Console.ReadKey().Key;
            }
        }

        private static async Task GetCustomerPrices()
        {
            using (var stoper = new Stoper())
            {
                var customerPrices = await _customerPriceService.GetCustomersPrices();

                DisplayCustomerPrices(customerPrices);
            }

            Console.WriteLine();
        }

        private static Task GetCustomerPricesRx()
        {
            return Task.CompletedTask;
        }

        private static async Task GetProductBasePrice()
        {
            var productService = new ProductPriceService();

            using (var stoper = new Stoper())
            {
                var productPrices = await productService.GetProductBasePrices();
                HappyConsole.WriteBlueLine($"Id --- Price --- Active");
                foreach (var item in productPrices)
                {
                    HappyConsole.WriteBlueLine($"{item.ProductId,2} - {item.Price.ToString("C"),9} - {(item.IsActive ? "A" : "")}");
                }
            }
        }

        private static Task GetProductBasePricesRx()
        {
            var productService = new ProductPriceService();

            HappyConsole.WriteBlueLine($"Id - Price - Active");

            return Task.CompletedTask;
        }

        private static void DisplayCustomerPrices(IList<CustomerPrice> customerPrices)
        {
            DisplayCustomerPricesHeader();
            foreach (var customerPrice in customerPrices)
            {
                DisplaySingleCustomerPrice(customerPrice);
            }

            DisplayCustomerPricesFooter();
        }

        private static void DisplayCustomerPricesFooter()
        {
            HappyConsole.WriteDarkGreenLine("==========================================");
        }

        private static void DisplaySingleCustomerPrice(CustomerPrice customerPrice)
        {
            HappyConsole.WriteGreenLine($"{customerPrice.CustomerId,2} {customerPrice.ProductId,13} {customerPrice.Price.ToString("C2"),17} {(customerPrice.IsActive ? "A" : ""),3}");
        }

        private static void DisplayCustomerPricesHeader()
        {
            HappyConsole.WriteDarkGreenLine("CustomerId == ProductId == Price == Active");
            HappyConsole.WriteDarkGreenLine("==========================================");
        }

        private static void DisplayMenu()
        {
            Console.Clear();
            HappyConsole.WriteDarkYellowLine("=== Menu:");
            HappyConsole.WriteDarkYellowLine("= 1 - GetCustomersPrices()");
            HappyConsole.WriteDarkYellowLine("= 2 - GetCustomersPricesRx()");
            HappyConsole.WriteDarkYellowLine("= 3 - GetProductBasePrices()");
            HappyConsole.WriteDarkYellowLine("= 4 - GetProductBasePricesRx()");
            HappyConsole.WriteDarkYellowLine("= 5 - Clear terminal");
            HappyConsole.WriteDarkYellowLine("== Esc - Exit");
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
