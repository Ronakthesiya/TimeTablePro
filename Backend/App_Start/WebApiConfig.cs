using Swashbuckle.Application;
using System.Web.Http;
using System.Web.Http.Cors;

namespace TimeTable_api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // global exception handler
            config.Filters.Add(new GlobalExceptionFilter());
            config.Filters.Add(new ModelValidatorFilter());

            // enable cors
            var cors = new EnableCorsAttribute(
                origins: "*",
                headers: "content-type,authorization",      // Matches your request exactly
                methods: "*"
            );
            cors.SupportsCredentials = true;                   // allowed methods
            cors.ExposedHeaders.Add("token");


            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();



            //Register default route
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );



        }
    }
}
