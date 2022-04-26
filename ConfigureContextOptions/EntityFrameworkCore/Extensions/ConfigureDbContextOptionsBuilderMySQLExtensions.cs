using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Demo.EntityFrameworkCore.Extensions
{
    public static class ConfigureDbContextOptionsBuilderMySQLExtensions
    {
        public static DbContextOptionsBuilder UseMySQL(this ConfigureDbContextOptionsBuilder builder,
            string connectionString,
            Action<MySqlDbContextOptionsBuilder> mySQLOptionsAction = null)
        {
            return builder.DbContextOptionsBuilder.UseMySql(connectionString,
                ServerVersion.AutoDetect(connectionString), optionsBuilder =>
                {
                    optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    mySQLOptionsAction?.Invoke(optionsBuilder);
                });
        }
    }
}
