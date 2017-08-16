using System;
using System.Linq;
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