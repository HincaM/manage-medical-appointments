using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persons.Api.App_Start;
using Persons.Api.Controllers;
using Persons.Domain.Interfaces;
using Persons.Infrastructure;
using Persons.Infrastructure.Services;
using System;
using System.Configuration;
using System.IdentityModel;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using static Persons.Application.DependencyInjections;

namespace Persons.Api
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

            services.AddTransient<DoctorsController>();
            services.AddTransient<PatientsController>();

            config.MapHttpAttributeRoutes();

            ServiceProvider = services.BuildServiceProvider();

            var context = ServiceProvider.GetRequiredService<PersonsContext>();
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
