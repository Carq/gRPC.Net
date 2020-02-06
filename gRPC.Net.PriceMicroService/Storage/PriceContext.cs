using gRPC.Net.PriceMicroService.Entities;
using Microsoft.EntityFrameworkCore;

namespace gRPC.Net.PriceMicroService.Storage
{
    public class PriceContext : DbContext
    {
        public DbSet<ProductPrice> ProductPrices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=Price.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductPrice>().ToTable("ProductPrices");
            modelBuilder.Entity<ProductPrice>().Ignore(x => x.IsActive);
        }
    }
}
