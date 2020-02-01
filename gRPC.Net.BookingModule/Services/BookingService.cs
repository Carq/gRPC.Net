using gRPC.Net.BookingModule.RxServices;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace gRPC.Net.BookingModule.Services
{
    public class BookingService : Bookings.BookingsBase
    {
        private readonly ILogger<BookingService> _logger;

        public BookingService(ILogger<BookingService> logger)
        {
            _logger = logger;
        }

        public override async Task GetBookings(BookingRequest request, IServerStreamWriter<BookingReply> responseStream, ServerCallContext context)
        {
            new BookingRxService()
                .GetBookingOfProductId()
                .Subscribe(async x => await responseStream.WriteAsync(new BookingReply { ProductId = 1, BookingCount = x }));

            await Task.Delay(500000);
        }
    }
}
