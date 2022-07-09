using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DeltaWare.SDK.Core.Caching
{
    public class AttributeCache : IAttributeCache
    {
        private readonly Dictionary<PropertyInfo, Dictionary<Type, List<Attribute>>> _propertyCache = new Dictionary<PropertyInfo, Dictionary<Type, List<Attribute>>>();
        private readonly Dictionary<Type, Dictionary<Type, List<Attribute>>> _typeCache = new Dictionary<Type, Dictionary<Type, List<Attribute>>>();

        public TAttribute[] GetAttributes<TAttribute>(Type type) where TAttribute : Attribute
        {
            if (_typeCache.TryGetValue(type, out Dictionary<Type, List<Attribute>> cache))
            {
                if (!cache.TryGetValue(typeof(TAttribute), out List<Attribute> cachedAttributes))
                {
                    return null;
                }

                return cachedAttributes.Cast<TAttribute>().ToArray();
            }

            List<TAttribute> foundAttributes = new List<TAttribute>();

            cache = new Dictionary<Type, List<Attribute>>();

            foreach (object attributeInstance in type.GetCustomAttributes(true))
            {
                if (attributeInstance is TAttribute foundAttribute)
                {
                    foundAttributes.Add(foundAttribute);
                }

                Type attributeType = attributeInstance.GetType();

                if (cache.TryGetValue(attributeType, out List<Attribute> attributeList))
                {
                    attributeList.Add((Attribute)attributeInstance);
                }
                else
                {
                    cache.Add(attributeType, new List<Attribute> { (Attribute)attributeInstance });
                }
            }

            _typeCache.Add(type, cache);

            if (foundAttributes.IsEmpty())
            {
                return null;
            }

            return foundAttributes.ToArray();
        }

        public TAttribute[] GetAttributes<TAttribute>(PropertyInfo property) where TAttribute : Attribute
        {
            if (_propertyCache.TryGetValue(property, out Dictionary<Type, List<Attribute>> cache))
            {
                if (!cache.TryGetValue(typeof(TAttribute), out List<Attribute> cachedAttributes))
                {
                    return null;
                }

                return cachedAttributes.Cast<TAttribute>().ToArray();
            }

            List<TAttribute> foundAttributes = new List<TAttribute>();

            cache = new Dictionary<Type, List<Attribute>>();

            foreach (object attributeInstance in property.GetCustomAttributes(true))
            {
                if (attributeInstance is TAttribute foundAttribute)
                {
                    foundAttributes.Add(foundAttribute);
                }

                Type attributeType = attributeInstance.GetType();

                if (cache.TryGetValue(attributeType, out List<Attribute> attributeList))
                {
                    attributeList.Add((Attribute)attributeInstance);
                }
                else
                {
                    cache.Add(attributeType, new List<Attribute> { (Attribute)attributeInstance });
                }
            }

            _propertyCache.Add(property, cache);

            if (foundAttributes.IsEmpty())
            {
                return null;
            }

            return foundAttributes.ToArray();
        }

        public bool HasAttribute<TAttribute>(Type type) where TAttribute : Attribute
        {
            return TryGetAttribute(type, out TAttribute _);
        }

        public bool HasAttribute<TAttribute>(PropertyInfo property) where TAttribute : Attribute
        {
            return TryGetAttribute(property, out TAttribute _);
        }

        public bool TryGetAttribute<TAttribute>(Type type, out TAttribute attribute) where TAttribute : Attribute
        {
            attribute = null;

            if (_typeCache.TryGetValue(type, out Dictionary<Type, List<Attribute>> cache))
            {
                if (!cache.TryGetValue(typeof(TAttribute), out List<Attribute> cachedAttributes))
                {
                    return false;
                }

                attribute = (TAttribute)cachedAttributes.Single();

                return true;
            }

            cache = new Dictionary<Type, List<Attribute>>();

            foreach (object attributeInstance in type.GetCustomAttributes(true))
            {
                if (attributeInstance is TAttribute foundAttribute)
                {
                    if (attribute != null)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    attribute = foundAttribute;
                }

                Type attributeType = attributeInstance.GetType();

                if (cache.TryGetValue(attributeType, out List<Attribute> attributeList))
                {
                    attributeList.Add((Attribute)attributeInstance);
                }
                else
                {
                    cache.Add(attributeType, new List<Attribute> { (Attribute)attributeInstance });
                }
            }

            _typeCache.Add(type, cache);

            return attribute != null;
        }

        public bool TryGetAttribute<TAttribute>(PropertyInfo property, out TAttribute attribute) where TAttribute : Attribute
        {
            attribute = null;

            if (_propertyCache.TryGetValue(property, out Dictionary<Type, List<Attribute>> cache))
            {
                if (!cache.TryGetValue(typeof(TAttribute), out List<Attribute> cachedAttributes))
                {
                    return false;
                }

                attribute = (TAttribute)cachedAttributes.First();

                return true;
            }

            cache = new Dictionary<Type, List<Attribute>>();

            foreach (object attributeInstance in property.GetCustomAttributes(true))
            {
                if (attributeInstance is TAttribute foundAttribute)
                {
                    if (attribute != null)
                    {
                        throw new ArgumentOutOfRangeException();
                    }

                    attribute = foundAttribute;
                }

                Type attributeType = attributeInstance.GetType();

                if (cache.TryGetValue(attributeType, out List<Attribute> attributeList))
                {
                    attributeList.Add((Attribute)attributeInstance);
                }
                else
                {
                    cache.Add(attributeType, new List<Attribute> { (Attribute)attributeInstance });
                }
            }

            _propertyCache.Add(property, cache);

            return attribute != null;
        }

        public bool TryGetAttributes<TAttribute>(PropertyInfo property, out TAttribute[] attributes) where TAttribute : Attribute
        {
            attributes = GetAttributes<TAttribute>(property);

            return attributes != null;
        }

        public bool TryGetAttributes<TAttribute>(Type type, out TAttribute[] attributes) where TAttribute : Attribute
        {
            attributes = GetAttributes<TAttribute>(type);

            return attributes != null;
        }
    }
}