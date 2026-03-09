using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persons.Domain.Interfaces;
using Persons.Infrastructure.Repositories;

namespace Persons.Infrastructure
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            IConnectionString connectionString = services.BuildServiceProvider().GetService<IConnectionString>();
            services
                .AddDbContext<PersonsContext>(options => options.UseSqlServer(connectionString.Value), ServiceLifetime.Scoped)
                .AddScoped<IDoctorsRepository, DoctorsRepository>()
                .AddScoped<IPatientsRepository, PatientsRepository>()
                ;
            return services;
        }
    }
}
