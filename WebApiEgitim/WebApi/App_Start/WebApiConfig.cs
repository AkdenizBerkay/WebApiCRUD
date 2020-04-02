using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using WebApi.Filters;
using WebApi.Helper;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            //Cors
            EnableCorsAttribute cors = new EnableCorsAttribute("http://localhost:55708", "*", "*");
            config.EnableCors(cors);

            //Filters
            config.Filters.Add(new YetkiKontrolu());
            config.Filters.Add(new ErrorAttribute());
            config.Filters.Add(new CrudLog());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
