WebApi-FluentValidation
=======================

Installation
--

Install from [Nuget](https://www.nuget.org/packages/RichardLawley.WebApi.FluentValidation/): 

    Install-Package RichardLawley.WebApi.OrderedFilters
    Install-Package RichardLawley.WebApi.FluentValidation

Configuration
--

In WebApiConfig.cs:

    // Change the Filter Provider to one which respects ordering
    config.Services.Replace(typeof(System.Web.Http.Filters.IFilterProvider), new OrderedFilterProvider());
    
    // Filters for FluentValidation
    config.Filters.Add(new FluentValidationActionFilter());     // Runs FluentValidation
    
    // Optional - prevent validation errors reaching controller (no need to check ModelState.IsValid)
    config.Filters.Add(new ValidationActionFilter());           // Prevents validation errors reaching controller
  
In your DI container initialisation (example uses autofac):

    // Configure the FluentValidation integration
    builder.RegisterType<ScopedValidatorFactory>().As<IScopedValidatorFactory>();
    builder.RegisterType<FluentValidatorProvider>().As<IFluentValidatorProvider>().SingleInstance();
    
Register any validators you need...

    builder.RegisterType<MyValidator>().As<IValidator<MyType>>();
