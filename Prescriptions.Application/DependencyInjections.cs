using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Prescriptions.Application
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddMediatR(typeof(DependencyInjections).Assembly)
                .AddValidatorsFromAssembly(typeof(DependencyInjections).Assembly)
                ;
            return services;
        }
    }
}
