using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.ModelBinding;
using Newtonsoft.Json;
using RichardLawley.WebApi.OrderedFilters;

namespace RichardLawley.WebApi.FluentValidation.Demo.Filters
{
    public class ValidationActionFilter : BaseActionFilterAttribute
    {
        // This must run AFTER the FluentValidation filter, which runs as 0
        public ValidationActionFilter() : base(1000) { }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var modelState = actionContext.ModelState;

            if (modelState.IsValid) return;

            List<ValidationFailure> errors = new List<ValidationFailure>();
            foreach (KeyValuePair<string, ModelState> item in actionContext.ModelState)
            {
                errors.AddRange(item.Value.Errors.Select(e => new ValidationFailure
                {
                    PropertyName = item.Key,
                    ErrorMessage = e.ErrorMessage
                }));
            }
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, errors);
        }
    }

    // The original class from FluentValidation has private setters
    public class ValidationFailure
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
    }
}
