using System.Threading.Tasks;
using Grpc.Net.Client;
using gRPC.Net.BookingModule;
using System.Threading;

namespace gRPC.Net.DynamicsPricing.Console
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new Bookings.BookingsClient(channel);
           
            using (var call = client.GetBookings(new BookingRequest { CustomerId = 1 }))
            {
                while (await call.ResponseStream.MoveNext(CancellationToken.None))
                {
                    BookingReply reply = call.ResponseStream.Current;
                    System.Console.WriteLine("Received " + reply.BookingCount);
                }
            }

            System.Console.WriteLine("Press any key to exit...");
            System.Console.ReadKey();
        }
    }
}
