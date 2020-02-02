using gRPC.Net.PriceMicroService;
using gRPC.Net.PriceMicroServiceTerminalClient.Helpers;
using Grpc.Net.Client;
using System;
using System.Threading.Tasks;

namespace gRPC.Net.PriceMicroServiceTerminalClient
{
    class Program
    {
        private static ProductPriceServicesGrpc.ProductPriceServicesGrpcClient _priceGrpc;

        static async Task Main(string[] args)
        {
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            _priceGrpc = new ProductPriceServicesGrpc.ProductPriceServicesGrpcClient(channel);

            DisplayMenu();

            var key = Console.ReadKey().Key;
            while (key != ConsoleKey.Escape)
            {
                Console.WriteLine();
                var task = key switch
                {
                    ConsoleKey.D1 => GetCustomerPrices(),
                    ConsoleKey.D2 => GetCustomerPricesRx(),
                    ConsoleKey.D5 => Task.Run(() => DisplayMenu()),
                    _ => Task.CompletedTask,
                };

                await task;
                key = Console.ReadKey().Key;
            }
        }

        private static async Task GetCustomerPrices()
        {
            using (var stope = new Stoper())
            {
                var result = await _priceGrpc.GetProductBasePricesAsync(new Google.Protobuf.WellKnownTypes.Empty());
                foreach (var item in result.ProductPrices)
                {
                    DisplayProduct(item);
                }
            }
        }

        private static async Task GetCustomerPricesRx()
        {
            throw new NotImplementedException();
        }

        private static void DisplayProduct(Product item)
        {
            HappyConsole.WriteBlueLine($"{item.ProductId,2} - {item.Price,5} - {item.IsActive}");
        }

        private static void DisplayMenu()
        {
            Console.Clear();
            HappyConsole.WriteDarkYellowLine("=== Menu:");
            HappyConsole.WriteDarkYellowLine("= 1 - GetProductBasePrices()");
            HappyConsole.WriteDarkYellowLine("= 2 - GetProductBasePricesRx()");
            HappyConsole.WriteDarkYellowLine("= 5 - Clear terminal");
            HappyConsole.WriteDarkYellowLine("== Esc - Exit");
        }
    }
}
