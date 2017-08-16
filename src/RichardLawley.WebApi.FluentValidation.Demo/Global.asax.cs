using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.WebApi;
using FluentValidation;
using RichardLawley.WebApi.FluentValidation.Demo.Models;
using RichardLawley.WebApi.OrderedFilters;

namespace RichardLawley.WebApi.FluentValidation.Demo
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Initialise Autofac
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);

            // Validator and the service on which it depends
            // NB: These validators could easily be Singleton as they have no inherent state, but API request
            // can be used for more complex validations which may involve a database lookup.
            builder.RegisterType<TestValidationService>().InstancePerRequest();
            builder.RegisterType<TestModelValidator>().As<IValidator<TestModel>>().InstancePerRequest();

            // Configure the FluentValidation integration
            builder.RegisterType<ScopedValidatorFactory>().As<IScopedValidatorFactory>();
            builder.RegisterType<FluentValidatorProvider>().As<IFluentValidatorProvider>().SingleInstance();

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Services.Replace(typeof(System.Web.Http.Filters.IFilterProvider), new OrderedFilterProvider());
        }
    }
}
