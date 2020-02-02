using Google.Protobuf.WellKnownTypes;
using gRPC.Net.PriceMicroService.ExternalService;
using gRPC.Net.PriceMicroService.Storage;
using Grpc.Core;
using System.Threading.Tasks;

namespace gRPC.Net.PriceMicroService.Services
{
    public class ProductPriceServices : ProductPriceServicesGrpc.ProductPriceServicesGrpcBase
    {
        private readonly ExternalProductApi _externalProductApi = new ExternalProductApi();

        private readonly PriceContext _priceContext = new PriceContext();

        public override Task<ProductBaseReply> GetProductBasePrices(Empty request, ServerCallContext context)
        {
            return Task.FromResult(new ProductBaseReply());
        }
    }
}
