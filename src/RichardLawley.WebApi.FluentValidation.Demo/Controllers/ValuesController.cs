using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using RichardLawley.WebApi.FluentValidation.Demo.Models;

namespace RichardLawley.WebApi.FluentValidation.Demo.Controllers
{
    public class ValuesController : ApiController
    {
        public string Post(TestModel model)
        {
            if (!ModelState.IsValid)
            {
                return "Failure, but still hit controller";
            }

            return "OK";
        }
    }
}
