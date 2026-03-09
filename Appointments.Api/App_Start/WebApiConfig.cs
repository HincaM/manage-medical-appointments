using Appointments.Api.App_Start;
using Appointments.Api.Controllers;
using Appointments.Domain.Interfaces;
using Appointments.Infrastructure;
using Appointments.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using static Appointments.Application.DependencyInjections;

namespace Appointments.Api
{
    public static class WebApiConfig
    {
        public static IServiceProvider ServiceProvider { get; private set; }
        public static void Register(HttpConfiguration config)
        {
            var services = new ServiceCollection();

            services.AddSingleton<IConnectionString>(new ConnectionString(
                ConfigurationManager
                    .ConnectionStrings["DefaultConnection"]
                    .ConnectionString
            ));

            services.AddApplication();
            services.AddInfrastructure();

            services.AddTransient<AppointmentsController>();

            config.MapHttpAttributeRoutes();

            ServiceProvider = services.BuildServiceProvider();

            var context = ServiceProvider.GetRequiredService<AppointmentsContext>();
            context.Database.Migrate();

            config.Services.Replace(
                typeof(IHttpControllerActivator),
                new ServiceProviderControllerActivator(ServiceProvider)
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
