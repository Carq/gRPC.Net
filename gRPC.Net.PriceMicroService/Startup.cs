using System.Linq;
using gRPC.Net.PriceMicroService.Entities;
using gRPC.Net.PriceMicroService.Services;
using gRPC.Net.PriceMicroService.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace gRPC.Net.PriceMicroService
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddGrpc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitializeDatabase();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<ProductPriceServices>();

                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
                });
            });
        }

        private void InitializeDatabase()
        {
            var priceContext = new PriceContext();
            priceContext.Database.EnsureCreated();

            if (priceContext.ProductPrices.Any())
            {
                return;
            }

            for (int i = 0; i <= 6; i++)
            {
                priceContext.ProductPrices.Add(new ProductPrice(i * 100, i + 10));
            }

            priceContext.SaveChanges();
        }
    }
}
