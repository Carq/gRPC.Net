using System;

namespace gRPC.Net.Terminal.ConsoleHelper
{
    public class ConsoleColor : IDisposable
    {
        private readonly System.ConsoleColor _previousColor;
        
        public ConsoleColor(System.ConsoleColor color)
        {
            _previousColor = Console.BackgroundColor;
            Console.BackgroundColor = color;
        }
        
        public void Dispose()
        {
            Console.BackgroundColor = _previousColor;
        }
    }
}
