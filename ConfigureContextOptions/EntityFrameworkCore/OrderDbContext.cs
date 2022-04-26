using Demo.Models;
using Microsoft.EntityFrameworkCore;

namespace Demo.EntityFrameworkCore
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureOrder();
        }

        public DbSet<Order> Orders { get; set; }
    }
}
