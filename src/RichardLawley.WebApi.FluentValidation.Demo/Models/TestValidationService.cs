using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RichardLawley.WebApi.FluentValidation.Demo.Models
{
    public class TestValidationService
    {
		public bool IsValid(int value) => value % 2 == 0;
	}
}