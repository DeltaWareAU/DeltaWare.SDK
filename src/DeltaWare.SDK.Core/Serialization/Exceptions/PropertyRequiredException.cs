using System;
using System.Reflection;

namespace DeltaWare.SDK.Core.Serialization.Exceptions
{
    public class PropertyRequiredException : Exception
    {
        public PropertyRequiredException(PropertyInfo property) : base($"{property.Name} must be not be null", new ArgumentNullException(property.Name))
        {
        }
    }
}