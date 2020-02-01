using System;

namespace gRPC.Net.Terminal.ConsoleHelper
{
    public static class HappyConsole
    {
        public static void WriteGreenLine(string text)
        {
            using (new ConsoleColor(System.ConsoleColor.DarkGreen))
            {
                Console.WriteLine(text);
            }
        }

        public static void WriteBlueLine(string text)
        {
            using (new ConsoleColor(System.ConsoleColor.DarkBlue))
            {
                Console.WriteLine(text);
            }
        }
    }
}
