using System.Threading.Tasks;
using Demo.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddMvc();

            var connectionString = Configuration.GetConnectionString("Default");
            services.AddDbContext<UserDbContext>(builder =>
            {
                builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.AddDbContext<OrderDbContext>(builder =>
            {
                builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.AddDbContext<ProductDbContext>(builder =>
            {
                builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger().UseSwaggerUI();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            Task.Run(async () =>
            {
                using (var services = app.ApplicationServices.CreateScope())
                {
                    await DatabaseInitializer.SeedAsync(services.ServiceProvider);
                }
            });
        }
    }
}
