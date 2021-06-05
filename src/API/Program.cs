using System;
using System.Threading.Tasks;
using API.Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
//using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;

namespace API
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                
                Log.Logger = new LoggerConfiguration()
                    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                    .Enrich.FromLogContext()
                    .WriteTo.Console()
                    .CreateLogger();

                try
                {
                    var context = services.GetRequiredService<ABCBankDbContext>();

                    if (context.Database.IsSqlServer())
                    {
                        await context.Database.MigrateAsync();
                    }
                    
                    await host.RunAsync();
                    return 0;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "An error occurred while migrating or seeding the database.");
                    Log.Fatal(ex, "Host terminated unexpectedly");

                    return 1;
                }
                finally
                {
                    Log.CloseAndFlush();
                }
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}