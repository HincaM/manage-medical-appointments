using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace Prescriptions.Api.App_Start
{
    public class ServiceProviderControllerActivator : IHttpControllerActivator
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderControllerActivator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IHttpController Create(
            HttpRequestMessage request,
            HttpControllerDescriptor controllerDescriptor,
            Type controllerType)
        {
            var scope = _serviceProvider.CreateScope();

            var controller = (IHttpController)scope.ServiceProvider
                .GetRequiredService(controllerType);

            request.RegisterForDispose(scope);

            return controller;
        }
    }
}
