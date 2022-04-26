using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.EntityFrameworkCore
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureProduct();
        }

        public DbSet<Product> Products { get; set; }
    }
}
