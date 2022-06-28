using System.Collections.Generic;
using System.Reflection;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class ObjectExtensions
    {
        /// <summary>
        /// Returns a <see cref="Dictionary{TKey,TValue}"/> using the properties of the <see cref="object"/> where the key is the property name and the value is the property value.
        /// </summary>
        /// <param name="value">The object whose properties should be returned as a <see cref="Dictionary{TKey,TValue}"/></param>
        public static Dictionary<string, object> GetPublicPropertiesAsDictionary(this object value)
        {
            Type objectType = value.GetType();

            Dictionary<string, object> propertyValues = new Dictionary<string, object>();

            foreach (PropertyInfo property in objectType.GetPublicProperties())
            {
                object propertyValue = property.GetValue(value);

                propertyValues.Add(property.Name, propertyValue);
            }

            return propertyValues;
        }
    }
}
