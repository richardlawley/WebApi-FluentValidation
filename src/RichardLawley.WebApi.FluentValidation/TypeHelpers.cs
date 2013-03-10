using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RichardLawley.WebApi.FluentValidation
{
    internal static class TypeHelpers
    {
        /// <summary>
        /// Determines whether the specified type is a Simple type
        /// </summary>
        /// <param name="type">The type.</param>
        public static bool IsSimpleType(this Type type)
        {
            return type.IsPrimitive ||
                   type.Equals(typeof(string)) ||
                   type.Equals(typeof(DateTime)) ||
                   type.Equals(typeof(Decimal)) ||
                   type.Equals(typeof(Guid)) ||
                   type.Equals(typeof(DateTimeOffset)) ||
                   type.Equals(typeof(TimeSpan));
        }

        /// <summary>
        /// Determines whether the specified type is a nullable value type
        /// </summary>
        /// <param name="type">The type.</param>
        public static bool IsNullableValueType(this Type type)
        {
            return Nullable.GetUnderlyingType(type) != null;
        }
    }
}
