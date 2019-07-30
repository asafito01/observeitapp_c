using ObserveItApp.Models;
using ObserveItApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Unity;
using Unity.AspNet.WebApi;
using Unity.Lifetime;

namespace ObserveItApp
{
        public static class WebApiConfig
        {
                public static void Register(HttpConfiguration config)
                {
                        // --------- Dependency injection resolver using Unity for WebApi
                        UnityContainer container = new UnityContainer();

                        // Inject persistent data
                        container.RegisterInstance<SchedulerModel>(new SchedulerModel(8, 17), new ContainerControlledLifetimeManager());

                        // Inject services
                        container.RegisterType<ISchedulerService, SchedulerService>(new ContainerControlledLifetimeManager());

                        config.DependencyResolver = new UnityDependencyResolver(container);

                        // --------- Web API configuration and services
                        // Web API routes
                        config.MapHttpAttributeRoutes();

                        config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();
                        config.Formatters.JsonFormatter.Indent = true;
                }
        }
}
