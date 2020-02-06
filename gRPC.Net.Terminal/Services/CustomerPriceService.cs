using gRPC.Net.Terminal.Entities;
using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace gRPC.Net.Terminal.Services
{
    public class CustomerPriceService
    {
        private readonly ProductPriceService _productPriceService = new ProductPriceService();

        public async Task<IList<CustomerPrice>> GetCustomersPrices()
        {
            var productPrices = await _productPriceService.GetProductBasePrices();

            var newCustomerPrices = new List<CustomerPrice>();

            foreach (var customerId in Customers.Ids)
            {
                foreach (var baseProductPrice in productPrices)
                {
                    var calculatedPrice = await CalculateCustomerPrice(customerId, baseProductPrice.Price);
                    var customerPrice = new CustomerPrice(customerId, baseProductPrice.ProductId, calculatedPrice, baseProductPrice.IsActive);
                    newCustomerPrices.Add(customerPrice);
                }
            }

            return newCustomerPrices;
        }

        public IObservable<CustomerPrice> GetCustomersPricesRx()
        {
            return _productPriceService
                .GetProductBasePricesRx()
                .Select(x => CreateCustomersPricesRx(x))
                .Merge();
        }

        public IObservable<CustomerPrice> CreateCustomersPricesRx(ProductPrice productPrice)
        {
            return Observable.Create<CustomerPrice>(async observer =>
            {
                foreach (var customerId in Customers.Ids)
                {
                    var calculatedPrice = await CalculateCustomerPrice(customerId, productPrice.Price);
                    observer.OnNext(new CustomerPrice(customerId, productPrice.ProductId, calculatedPrice, productPrice.IsActive));
                }
            });
        }


        private async Task<double> CalculateCustomerPrice(int customerId, double newPrice)
        {
            await Task.Delay(100);
            return newPrice - (Customers.Promotion[customerId] * newPrice);
        }
    }
}
