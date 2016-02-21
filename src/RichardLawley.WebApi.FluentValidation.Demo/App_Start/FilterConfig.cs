using System.Web.Mvc;

namespace RichardLawley.WebApi.FluentValidation.Demo
{
    public static class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}