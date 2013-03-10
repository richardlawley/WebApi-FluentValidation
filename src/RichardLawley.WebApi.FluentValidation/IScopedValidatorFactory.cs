using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Http.Dependencies;
using FluentValidation;

namespace RichardLawley.WebApi.FluentValidation
{
    public interface IScopedValidatorFactory
    {
        /// <summary>
        /// Gets the validator for the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IValidator<T> GetValidator<T>(IDependencyScope scope);

        /// <summary>
        /// Gets the validator for the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        IValidator GetValidator(Type type, IDependencyScope scope);
    }
}
