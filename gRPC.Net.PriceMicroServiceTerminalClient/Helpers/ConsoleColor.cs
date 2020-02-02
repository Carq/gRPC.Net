using System;

namespace gRPC.Net.PriceMicroServiceTerminalClient.Helpers
{
    public class ConsoleColor : IDisposable
    {
        private readonly System.ConsoleColor _previousColor;

        public ConsoleColor(System.ConsoleColor color)
        {
            _previousColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
        }

        public void Dispose()
        {
            Console.ForegroundColor = _previousColor;
        }
    }
}