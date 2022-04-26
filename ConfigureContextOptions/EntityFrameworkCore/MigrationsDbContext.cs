using Microsoft.EntityFrameworkCore;

namespace Demo.EntityFrameworkCore
{
    public class MigrationsDbContext : DbContext
    {
        public MigrationsDbContext(DbContextOptions<MigrationsDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ConfigureUser();
            modelBuilder.ConfigureOrder();
            modelBuilder.ConfigureProduct();
        }
    }
}
