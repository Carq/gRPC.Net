using gRPC.Net.Terminal.Entities;
using gRPC.Net.Terminal.ExternalService;
using gRPC.Net.Terminal.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
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

            foreach (var singleProductPrice in productPrices)
            {
                singleProductPrice.IsActive = await _externalProductApi.IsProductActive(singleProductPrice.ProductId);
            }

            return productPrices;
        }

        public IObservable<ProductPrice> GetProductBasePricesRx()
        {
            var productPrices = _priceContext.ProductPrices.ToList();

            return Observable.Create<ProductPrice>(async observer =>
            {
                foreach (var singleProductPrice in productPrices)
                {
                    singleProductPrice.IsActive = await _externalProductApi.IsProductActive(singleProductPrice.ProductId);
                    observer.OnNext(singleProductPrice);
                }

                observer.OnCompleted();
            });
        }
    }
}
