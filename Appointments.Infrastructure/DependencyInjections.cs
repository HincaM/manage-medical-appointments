using Appointments.Application.Interfaces;
using Appointments.Domain.Interfaces;
using Appointments.Infrastructure.Repositories;
using Appointments.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Appointments.Infrastructure
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            IConnectionString connectionString = services.BuildServiceProvider().GetService<IConnectionString>();
            services
                .AddDbContext<AppointmentsContext>(options => options.UseSqlServer(connectionString.Value), ServiceLifetime.Scoped)
                .AddScoped<IAppointmentRepository, AppointmentRepository>()
                .AddScoped<IEventBus, RabbitMqEventBus>()
                ;
            return services;
        }
    }
}
