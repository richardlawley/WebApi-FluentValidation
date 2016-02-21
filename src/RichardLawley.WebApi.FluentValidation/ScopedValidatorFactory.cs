using System;
using System.Linq;
using System.Web.Http.Dependencies;
using FluentValidation;

namespace RichardLawley.WebApi.FluentValidation
{
    /// <summary>
    /// Resolves Validators from a Scope
    /// </summary>
    public class ScopedValidatorFactory : IScopedValidatorFactory
    {
        public IValidator<T> GetValidator<T>(IDependencyScope scope) => GetValidator(typeof(T), scope) as IValidator<T>;

        public IValidator GetValidator(Type type, IDependencyScope scope) => scope.GetService(type) as IValidator;
    }
}