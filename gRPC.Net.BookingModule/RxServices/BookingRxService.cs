using System;
using System.Reactive.Linq;

namespace gRPC.Net.BookingModule.RxServices
{
    public class BookingRxService
    {
        public IObservable<int> GetBookingOfProductId()
        {
            int i = 0;
            return Observable.Create<int>(
                    observer =>
                    {
                        var timer = new System.Timers.Timer();
                        timer.Interval = 100;
                        timer.Elapsed += (s, e) =>
                        {
                            observer.OnNext(i++);
                        };

                        timer.Start();
                        return timer;
                    });
        }
    }
}
