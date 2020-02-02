using System;

namespace gRPC.Net.Terminal.Helpers
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

        public static void WriteGrayLine(string text)
        {
            using (new ConsoleColor(System.ConsoleColor.DarkGray))
            {
                Console.WriteLine(text);
            }
        }
    }
}
