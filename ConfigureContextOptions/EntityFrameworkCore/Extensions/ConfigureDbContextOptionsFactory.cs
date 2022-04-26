using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Demo.EntityFrameworkCore.Extensions
{
    public static class ConfigureDbContextOptionsFactory
    {
        public static DbContextOptions<TDbContext> CreateContextOptions<TDbContext>(IServiceProvider serviceProvider)
            where TDbContext : DbContext
        {
            var options = serviceProvider.GetRequiredService<IOptions<ConfigureDbContextOptions>>().Value;
            var builder = new ConfigureDbContextOptionsBuilder<TDbContext>(serviceProvider, "");

            var configureAction = options.ConfigureActions
                .TryGetValue(typeof(TDbContext), out object obj) ? obj : default;
            if (configureAction != null)
            {
                ((Action<ConfigureDbContextOptionsBuilder<TDbContext>>)configureAction).Invoke(builder);
            }
            else if (options.ConfigureAction != null)
            {
                options.ConfigureAction.Invoke(builder);
            }
            else
            {
                throw new Exception($"No configuration found for {typeof(DbContext).AssemblyQualifiedName}!");
            }

            return builder.DbContextOptionsBuilder.Options;
        }
    }
}
