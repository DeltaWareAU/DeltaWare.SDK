using DeltaWare.SDK.Serialization.Types.Transformation.Exceptions;
using System;
using System.Globalization;

namespace DeltaWare.SDK.Serialization.Types.Transformation
{
    /// <summary>
    /// Transforms a string to a object.
    /// </summary>
    public interface ITransformer
    {
        Type Type { get; }

        /// <summary>
        /// Transforms the specified string to an object.
        /// </summary>
        /// <param name="value">The value to be transformed.</param>
        /// <param name="toType">The type being transformed.</param>
        /// <param name="culture">Specifies the culture.</param>
        /// <exception cref="InvalidTransformationTypeException">
        /// Thrown when an invalid type is provided.
        /// </exception>
        /// <exception cref="InvalidTransformationException">
        /// Thrown when an invalid value is provided.
        /// </exception>
        object TransformToObject(string value, Type toType, CultureInfo culture = null);

        string TransformToString(object value, Type type, CultureInfo culture = null);
    }
}