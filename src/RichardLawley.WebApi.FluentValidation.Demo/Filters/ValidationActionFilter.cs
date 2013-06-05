using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
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

            var errors = actionContext.ModelState
                .SelectMany(v => v.Value.Errors
                    .Select(e => JsonConvert.DeserializeObject<ValidationFailure>(e.ErrorMessage)))
                        .ToArray();

            actionContext.Response = actionContext.Request
                .CreateResponse(HttpStatusCode.BadRequest, errors);
        }
    }
	
	// The original class from FluentValidation has private setters
    public class ValidationFailure
    {
        public string PropertyName { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorCode { get; set; }
        public object AttemptedValue { get; set; }
        public object CustomState { get; set; }
    }
}
