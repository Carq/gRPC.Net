using gRPC.Net.Terminal.Entities;
using gRPC.Net.Terminal.ExternalService;
using gRPC.Net.Terminal.Storage;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace gRPC.Net.Terminal.Services
{
    public class ProductPriceService
    {
        private readonly PriceContext _priceContext = new PriceContext();

        private readonly ExternalProductApi _externalProductApi = new ExternalProductApi();

        public async Task<IList<ProductPrice>> GetProductBasePrices()
        {
            var productPrices = await _priceContext.ProductPrices.ToListAsync();

            var activeProductPrices = new List<ProductPrice>();
            foreach (var singleProductPrice in productPrices)
            {
                if (await _externalProductApi.IsProductActive(singleProductPrice.ProductId))
                {
                    activeProductPrices.Add(singleProductPrice);
                }
            }

            return activeProductPrices;
        }
    }
}
