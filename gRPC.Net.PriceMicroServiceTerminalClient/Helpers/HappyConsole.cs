using System;

namespace gRPC.Net.PriceMicroServiceTerminalClient.Helpers
{
    public static class HappyConsole
    {
        public static void WriteDarkGreenLine(string text)
        {
            using (new ConsoleColor(System.ConsoleColor.DarkGreen))
            {
                Console.WriteLine(text);
            }
        }

        public static void WriteGreenLine(string text)
        {
            using (new ConsoleColor(System.ConsoleColor.Green))
            {
                Console.WriteLine(text);
            }
        }

        public static void WriteDarkGrayLine(string text)
        {
            using (new ConsoleColor(System.ConsoleColor.DarkGray))
            {
                Console.WriteLine(text);
            }
        }

        public static void WriteDarkYellowLine(string text)
        {
            using (new ConsoleColor(System.ConsoleColor.DarkYellow))
            {
                Console.WriteLine(text);
            }
        }

        public static void WriteBlueLine(string text)
        {
            using (new ConsoleColor(System.ConsoleColor.Blue))
            {
                Console.WriteLine(text);
            }
        }

        public static void WriteCyanLine(string text)
        {
            using (new ConsoleColor(System.ConsoleColor.Cyan))
            {
                Console.WriteLine(text);
            }
        }
    }
}
