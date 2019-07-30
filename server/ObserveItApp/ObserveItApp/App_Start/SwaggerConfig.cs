using System.Web.Http;
using WebActivatorEx;
using ObserveItApp;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace ObserveItApp
{
        public class SwaggerConfig
        {
                public static void Register()
                {
                        var thisAssembly = typeof(SwaggerConfig).Assembly;
                        GlobalConfiguration.Configuration
                            .EnableSwagger(c => { c.SingleApiVersion("v1", "ObserveItApp"); })
                            .EnableSwaggerUi(c => { });
                }
        }
}
