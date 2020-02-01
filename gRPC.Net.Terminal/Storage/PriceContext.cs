using gRPC.Net.Terminal.Entities;
using Microsoft.EntityFrameworkCore;

namespace gRPC.Net.Terminal.Storage
{
    public class PriceContext : DbContext
    {
        public DbSet<BasePrice> BasePrices { get; set; }

        public DbSet<CustomerPrice> CustomerPrices { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=Price.db;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BasePrice>().ToTable("BasePrices");
            modelBuilder.Entity<CustomerPrice>().ToTable("CustomerPrices");
        }
    }
}
