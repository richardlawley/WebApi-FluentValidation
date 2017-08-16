using System;
using System.Linq;

namespace RichardLawley.WebApi.FluentValidation.Demo.Models
{
    public class TestValidationService
    {
        public bool IsValid(int value) => value % 2 == 0;
    }
}