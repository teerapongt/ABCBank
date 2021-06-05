using API.Application.Common.Interfaces;
using API.Infrastructure.Persistence;
using API.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ABCBankDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ABCBankDbContext).Assembly.FullName)
                )
            );
            services.AddScoped<IABCBankDbContext>(provider => provider.GetService<ABCBankDbContext>());
            services.AddTransient<ICurrentUser, CurrentUser>();
            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IIBAN, IBANService>();
            services.AddTransient<IFee, FeeService>();

            return services;
        }
    }
}