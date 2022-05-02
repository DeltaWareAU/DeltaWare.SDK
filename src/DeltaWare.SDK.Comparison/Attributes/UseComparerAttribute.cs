using System;

namespace DeltaWare.SDK.Comparison.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class UseComparerAttribute : Attribute, IObjectComparer
    {
        public IObjectComparer Comparer { get; }

        public UseComparerAttribute(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }

            if (!type.IsClass)
            {
                throw new ArgumentException($"{type.FullName} must be a class");
            }

            if (!type.ImplementsInterface<IObjectComparer>())
            {
                throw new ArgumentException($"{type.FullName} must implement {nameof(type)}");
            }

            Comparer = (IObjectComparer)Activator.CreateInstance(type);
        }

        public bool Compare(object valueA, object valueB)
        {
            return Comparer.Compare(valueA, valueB);
        }
    }
}