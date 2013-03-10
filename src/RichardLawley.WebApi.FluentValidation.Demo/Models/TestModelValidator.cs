using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation;

namespace RichardLawley.WebApi.FluentValidation.Demo.Models
{
    public class TestModelValidator : AbstractValidator<TestModel>
    {
        public TestModelValidator(TestValidationService service)
        {
            RuleFor(m => m.Value).Must(v => service.IsValid(v));
        }
    }
}