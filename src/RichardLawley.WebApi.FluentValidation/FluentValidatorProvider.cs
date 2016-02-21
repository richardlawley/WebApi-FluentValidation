using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Http.Dependencies;
using FluentValidation;

namespace RichardLawley.WebApi.FluentValidation
{
    /// <summary>
    /// Provides FluentValidators appropriate for a type, caching which types have validators
    /// </summary>
    public class FluentValidatorProvider : IFluentValidatorProvider
    {
        private readonly HashSet<Type> _typesWithValidators = new HashSet<Type>();
        private readonly HashSet<Type> _typesWithNoValidators = new HashSet<Type>();
        private readonly object _syncRoot = new object();
        private readonly IScopedValidatorFactory _factory;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="factory">Validator factory</param>
        public FluentValidatorProvider(IScopedValidatorFactory factory)
        {
            _factory = factory;
        }

        /// <summary>
        /// Provides any FluentValidation Validators appropriate for validating the specified type.  These will have
        /// been created within the specified Dependency Scope
        /// </summary>
        /// <param name="type">Model type to find validators for</param>
        /// <param name="scope">Scope to create validators from</param>
        /// <returns></returns>
        public IEnumerable<IValidator> GetValidators(Type type, IDependencyScope scope)
        {
            Trace.TraceInformation("GetValidators: {0}", type.FullName);

            if (_typesWithNoValidators.Contains(type))
            {
                return Enumerable.Empty<IValidator>();
            }

            IValidator validator = null;
            Type validatorType = typeof(IValidator<>).MakeGenericType(type);

            if (!_typesWithValidators.Contains(type))
            {
                // We've not checked this type before.  Look for a validator and store whether we have one
                lock (_syncRoot)
                {
                    validator = _factory.GetValidator(validatorType, scope);
                    if (validator == null)
                    {
                        _typesWithNoValidators.Add(type);
                        return Enumerable.Empty<IValidator>();
                    }
                    else
                    {
                        _typesWithValidators.Add(type);
                    }
                }
            }

            // There should be a validator available for this type
            if (validator == null)
            {
                validator = scope.GetService(validatorType) as IValidator;
            }
            return new[] { validator };
        }
    }
}