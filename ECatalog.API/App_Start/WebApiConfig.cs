using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using ECatalog.API.Infrastructure;
using Newtonsoft.Json.Serialization;

namespace ECatalog.API
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            config.EnableCors(new EnableCorsAttribute("*", "*", "GET,POST"));
            // Web API configuration and services
            UnityConfig.RegisterTypes(config);

            // Web API routes
            config.MapHttpAttributeRoutes();
            config.MessageHandlers.Add(new LanguageMessageHandler());
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Web API configuration and services
            config.Formatters.Remove(GlobalConfiguration.Configuration.Formatters.XmlFormatter);
            // Use camel case for JSON data.
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
