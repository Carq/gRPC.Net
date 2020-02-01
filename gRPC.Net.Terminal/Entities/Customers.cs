using System.Collections.Generic;

namespace gRPC.Net.Terminal.Entities
{
    public static class Customers
    {
        public static readonly int[] Ids = new[]
        {
            1,
            2,
            3,
        };

        public static readonly IDictionary<int, double> Promotion = new Dictionary<int, double>
        {
            { 1, 0.40d },
            { 2, 0.10d },
            { 3, 0.20d },
        };
    }
}
