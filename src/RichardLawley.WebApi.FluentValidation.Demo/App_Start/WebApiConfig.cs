using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using RichardLawley.WebApi.FluentValidation.Demo.Filters;

namespace RichardLawley.WebApi.FluentValidation.Demo
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Filters for FluentValidation
            config.Filters.Add(new FluentValidationActionFilter());     // Runs FluentValidation
            config.Filters.Add(new ValidationActionFilter());           // Prevents validation errors reaching controller
        }
    }
}
