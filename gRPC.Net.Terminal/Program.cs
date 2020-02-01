using gRPC.Net.Terminal.Services;
using gRPC.Net.Terminal.Storage;
using System;
using System.Threading.Tasks;

namespace gRPC.Net.Terminal
{
    class Program
    {
        private static readonly BasePriceService _basePriceService = new BasePriceService();

        private static readonly PriceContext _priceContext = new PriceContext();

        static async Task Main(string[] args)
        {
            await _priceContext.Database.EnsureCreatedAsync();

            await _basePriceService.ChangeBasePrice(1, 50);
            
            Console.ReadKey();
        }
    }
}
