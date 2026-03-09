using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Prescriptions.Api.App_Start;
using Prescriptions.Api.Consumers;
using Prescriptions.Api.Controllers;
using Prescriptions.Application.Features.Prescriptions.Create;
using Prescriptions.Domain.Interfaces;
using Prescriptions.Infrastructure;
using Prescriptions.Infrastructure.Services;
using System;
using System.Configuration;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using static Prescriptions.Application.DependencyInjections;

namespace Prescriptions.Api
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

            services.AddSingleton<IUrlPersons>(new UrlPersons(ConfigurationManager.AppSettings["UrlPersons"]));

            services.AddApplication();
            services.AddInfrastructure();

            services.AddTransient<PrescriptionsController>();

            config.MapHttpAttributeRoutes();

            ServiceProvider = services.BuildServiceProvider();

            var context = ServiceProvider.GetRequiredService<PrescriptionsContext>();
            context.Database.Migrate();

            var consumer = new PrescriptionsConsumer("prescriptions", async (message) =>
            {
                IMediator mediator = ServiceProvider.GetService<IMediator>();
                var evento = JsonConvert.DeserializeObject<CreatePrescriptionCommand>(message);
                await mediator.Send(evento);
                return true;
            });

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
