using System.Linq;
using System.Threading.Tasks;

namespace gRPC.Net.Terminal.ExternalService
{
    public class ExternalProductApi
    {
        public static readonly int[] InactiveProducts = new[]
        {
            10,
            12,
            16,
        };

        public async Task<bool> IsProductActive(int productId)
        {
            await Task.Delay(150);
            return !InactiveProducts.Contains(productId);
        }
    }
}
