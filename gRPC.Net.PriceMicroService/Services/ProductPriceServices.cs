using Google.Protobuf.WellKnownTypes;
using gRPC.Net.PriceMicroService.ExternalService;
using gRPC.Net.PriceMicroService.Storage;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace gRPC.Net.PriceMicroService.Services
{
    public class ProductPriceServices : ProductPriceServicesGrpc.ProductPriceServicesGrpcBase
    {
        private readonly ExternalProductApi _externalProductApi = new ExternalProductApi();

        private readonly PriceContext _priceContext = new PriceContext();

        public override async Task<ProductBaseReply> GetProductBasePrices(Empty request, ServerCallContext context)
        {
            var productPrices = await _priceContext.ProductPrices.ToListAsync();
            var res = new ProductBaseReply();

            foreach (var singleProductPrice in productPrices)
            {
                singleProductPrice.IsActive = await _externalProductApi.IsProductActive(singleProductPrice.ProductId);
                res.ProductPrices.Add(new Product()
                    {
                        Price = singleProductPrice.Price,
                        IsActive = singleProductPrice.IsActive,
                        ProductId = singleProductPrice.ProductId,
                    });
            }

            return res;
        }

        public override async Task GetProductBasePricesRx(Empty request, IServerStreamWriter<Product> responseStream, ServerCallContext context)
        {
            var productPrices = await _priceContext.ProductPrices.ToListAsync();
            foreach (var singleProductPrice in productPrices)
            {
                singleProductPrice.IsActive = await _externalProductApi.IsProductActive(singleProductPrice.ProductId);
                await responseStream.WriteAsync(new Product()
                {
                    Price = singleProductPrice.Price,
                    IsActive = singleProductPrice.IsActive,
                    ProductId = singleProductPrice.ProductId,
                });
            }
        }
    }
}
