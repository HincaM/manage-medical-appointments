using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Prescriptions.Domain.Interfaces;
using Prescriptions.Domain.Services;
using Prescriptions.Infrastructure.Repositories;
using Prescriptions.Infrastructure.Services;

namespace Prescriptions.Infrastructure
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            IConnectionString connectionString = services.BuildServiceProvider().GetService<IConnectionString>();
            services
                .AddDbContext<PrescriptionsContext>(options => options.UseSqlServer(connectionString.Value), ServiceLifetime.Scoped)
                .AddScoped<IPrescriptionsRepository, PrescriptionsRepository>()
                .AddScoped<IHttpService, HttpService>()
                ;
            return services;
        }
    }
}
