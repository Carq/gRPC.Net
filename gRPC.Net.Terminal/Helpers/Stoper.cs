using System;
using System.Diagnostics;

namespace gRPC.Net.Terminal.Helpers
{
    public class Stoper : IDisposable
    {
        private readonly Stopwatch _watch;

        public Stoper()
        {
            HappyConsole.WriteGrayLine($"In progress...");
            _watch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            _watch.Stop();
            HappyConsole.WriteGrayLine($"Done: {_watch.ElapsedMilliseconds}ms - {_watch.ElapsedMilliseconds / 1000.0m:0.##} s");
        }
    }
}
