using Appointments.Application.Features.Appointments.StartAppointment;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Appointments.Application
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(typeof(StartAppointmentCommand).Assembly);

            services.AddValidatorsFromAssembly(typeof(DependencyInjections).Assembly);

            return services;
        }
    }
}
