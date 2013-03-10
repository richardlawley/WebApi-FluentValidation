using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using RichardLawley.WebApi.OrderedFilters;

namespace RichardLawley.WebApi.FluentValidation.Demo.Filters
{
    public class ValidationActionFilter : BaseActionFilterAttribute
    {
        // This must run AFTER the FluentValidation filter, which runs as 0
        public ValidationActionFilter() : base(1000) { }

        public override void OnActionExecuting(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;
            if (!modelState.IsValid)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.BadRequest, "One or more validation errors occurred");
                actionContext.Response.ReasonPhrase = "One or more validation errors occurred";
            }
        }
    }
}