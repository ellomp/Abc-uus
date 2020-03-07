using Abc.Infra.Quantity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Abc.Soft
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build(); //võtan hosti
            using (var scope = host.Services.CreateScope()) //vaatan mis teenused hostis on
            {
                var services = scope.ServiceProvider; //loon endale kõik need teenused mida vaja
                var dbQuantity = services.GetRequiredService<QuantityDbContext>();
                QuantityDbInitializer.Initialize(dbQuantity);
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
