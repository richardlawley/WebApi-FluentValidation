using System;
using System.Linq;

namespace RichardLawley.WebApi.FluentValidation
{
    internal static class TypeHelpers
    {
        /// <summary>
        /// Determines whether the specified type is a Simple type
        /// </summary>
        /// <param name="type">The type.</param>
        public static bool IsSimpleType(this Type type) => type.IsPrimitive
            || type.Equals(typeof(string))
            || type.Equals(typeof(DateTime))
            || type.Equals(typeof(Decimal))
            || type.Equals(typeof(Guid))
            || type.Equals(typeof(DateTimeOffset))
            || type.Equals(typeof(TimeSpan));

        /// <summary>
        /// Determines whether the specified type is a nullable value type
        /// </summary>
        /// <param name="type">The type.</param>
        public static bool IsNullableValueType(this Type type) => Nullable.GetUnderlyingType(type) != null;
    }
}