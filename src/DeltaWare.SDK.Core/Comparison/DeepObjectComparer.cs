using DeltaWare.SDK.Core.Caching;
using DeltaWare.SDK.Core.Comparison.Attributes;
using DeltaWare.SDK.Core.Comparison.Types;
using DeltaWare.SDK.Core.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Core.Comparison
{
    public class DeepObjectComparer : IDeepObjectComparer
    {
        private readonly IAttributeCache _attributeCache;
        private readonly IObjectComparer _defaultComparer;
        private readonly IObjectSerializer _objectSerializer;

        public DeepObjectComparer(IObjectComparer objectComparer = null)
        {
            _defaultComparer = objectComparer ?? new ObjectComparer();

            _attributeCache = new AttributeCache();
            _objectSerializer = new ObjectSerializer(_attributeCache);
        }

        public ObjectComparisonResults Compare(object valueA, object valueB, string name = null)
        {
            if (valueA == null || valueB == null)
            {
                ObjectComparisonResults comparisonResult = new(name);

                if (_defaultComparer.Compare(valueA, valueB))
                {
                    comparisonResult.RegisterValue(new ValueComparisonResult(name, true));
                }
                else
                {
                    comparisonResult.RegisterValue(new ValueComparisonResult(name, false, valueA, valueB));
                }

                comparisonResult.Finalise();

                return comparisonResult;
            }

            Type type = valueA.GetType();

            if (string.IsNullOrWhiteSpace(name))
            {
                name = type.Name;
            }

            PropertyInfo[] properties = type.GetProperties();

            ObjectComparisonResults comparisonResults = new(name);

            foreach (PropertyInfo property in properties)
            {
                object instanceA = property.GetValue(valueA);
                object instanceB = property.GetValue(valueB);

                if (instanceA == null)
                {
                    if (instanceB == null)
                    {
                        continue;
                    }

                    CompareObject(instanceA, instanceB, property);

                    continue;
                }

                if (_objectSerializer.CanSerialize(property))
                {
                    CompareObject(instanceA, instanceB, property);
                }
                else if (property.PropertyType.ImplementsInterface<IList>())
                {
                    comparisonResults.RegisterObject(CompareList(instanceA, instanceB, property.Name));
                }
                else
                {
                    comparisonResults.RegisterObject(Compare(instanceA, instanceB, property.Name));
                }
            }

            comparisonResults.Finalise();

            return comparisonResults;

            #region Helper Methods

            void CompareObject(object instanceA, object instanceB, PropertyInfo property)
            {
                bool matching;

                if (_attributeCache.TryGetAttribute(property, out DoNotCompareAttribute _))
                {
                    matching = true;
                }
                else if (_attributeCache.TryGetAttribute(property, out UseComparerAttribute comparerOverride))
                {
                    matching = comparerOverride.Compare(instanceA, instanceB);
                }
                else
                {
                    matching = _defaultComparer.Compare(instanceA, instanceB);
                }

                if (matching)
                {
                    comparisonResults.RegisterValue(new ValueComparisonResult(property.Name, true));
                }
                else
                {
                    comparisonResults.RegisterValue(new ValueComparisonResult(property.Name, false, instanceA, instanceB));
                }
            }

            #endregion Helper Methods
        }

        public Task<ObjectComparisonResults> CompareAsync(object valueA, object valueB, string name = null)
        {
            return Task.FromResult(Compare(valueA, valueB, name));
        }

        public ObjectComparisonResults CompareMany(IEnumerable<object> comparisonA, IEnumerable<object> comparisonB, string name = null)
        {
            return CompareList(comparisonA.ToList(), comparisonB.ToList(), name);
        }

        public Task<ObjectComparisonResults> CompareManyAsync(IEnumerable<object> comparisonA, IEnumerable<object> comparisonB, string name = null)
        {
            return Task.FromResult(CompareList(comparisonA.ToList(), comparisonB.ToList(), name));
        }

        private ObjectComparisonResults CompareList(object valueA, object valueB, string name)
        {
            if (valueA != null)
            {
            }

            IList listA = (IList)valueA ?? new ArrayList();
            IList listB = (IList)valueB ?? new ArrayList();

            ObjectComparisonResults listComparisonResults = new(name);

            // The supplied lists may not be the same size.
            if (listA.Count >= listB.Count)
            {
                // A list is equal to or greater than the B list.
                for (int i = 0; i < listA.Count; i++)
                {
                    if (i < listB.Count)
                    {
                        // We can compare each index of the list.
                        listComparisonResults.RegisterObject(Compare(listA[i], listB[i], $"{name}[{i}]"));
                    }
                    else
                    {
                        // We can compare only the a list.
                        listComparisonResults.RegisterObject(Compare(listA[i], null, $"{name}[{i}]"));
                    }
                }
            }
            else
            {
                // B list is greater than the A list.
                for (int i = 0; i < listB.Count; i++)
                {
                    if (i < listA.Count)
                    {
                        listComparisonResults.RegisterObject(Compare(listA[i], listB[i], $"{name}[{i}]"));
                    }
                    else
                    {
                        // We can compare only the b list.
                        listComparisonResults.RegisterObject(Compare(null, listB[i], $"{name}[{i}]"));
                    }
                }
            }

            listComparisonResults.Finalise();

            return listComparisonResults;
        }
    }
}