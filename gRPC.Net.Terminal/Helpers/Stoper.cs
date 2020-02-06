using System;
using System.Diagnostics;

namespace gRPC.Net.Terminal.Helpers
{
    public class Stoper : IDisposable
    {
        private readonly Stopwatch _watch;

        public Stoper()
        {
            HappyConsole.WriteDarkGrayLine($"In progress...");
            _watch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            _watch.Stop();
            HappyConsole.WriteDarkGrayLine($"Done: {_watch.ElapsedMilliseconds}ms - {_watch.ElapsedMilliseconds / 1000.0m:0.##} s");
        }
    }
}
