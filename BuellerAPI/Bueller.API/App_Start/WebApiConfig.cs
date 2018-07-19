using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Bueller.API
{
    public static class WebApiConfig
    {
        public static string AuthenticationType = "AuthTestCookie";
        public static string CookieName = "AuthTestCookie";

        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            var corsattr = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(corsattr);

            config.Filters.Add(new AuthorizeAttribute());

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
