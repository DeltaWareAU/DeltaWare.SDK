using DeltaWare.SDK.Comparison.Exceptions;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace DeltaWare.SDK.Comparison.Types
{
    [DebuggerDisplay("Matching:{Matching.ToString().ToUpper()} - {Name} | [Values:{_values.Count} - Objects:{_objects.Count}]")]
    public class ObjectComparisonResults
    {
        private readonly List<ObjectComparisonResults> _objects = new();
        private readonly List<ValueComparisonResult> _values = new();
        private readonly bool isFinalised = false;
        public bool Matching { get; private set; } = true;
        public string Name { get; }
        public ObjectComparisonResults[] Objects { get; private set; }
        public ValueComparisonResult[] Values { get; private set; }

        public ObjectComparisonResults(string name)
        {
            Name = name;
        }

        public void Finalise()
        {
            if (isFinalised)
            {
                return;
            }

            Objects = _objects.ToArray();
            Values = _values.ToArray();
        }

        public ValueComparisonResult[] GetFailingComparisons()
        {
            List<ValueComparisonResult> failingComparisons = Objects.SelectMany(o => o.GetFailingComparisons()).ToList();

            failingComparisons.AddRange(Values.Where(v => !v.Matching));

            return failingComparisons.ToArray();
        }

        public void RegisterObject(ObjectComparisonResults objectResult)
        {
            _objects.Add(objectResult);

            if (Matching)
            {
                Matching = objectResult.Matching;
            }
        }

        public void RegisterValue(ValueComparisonResult valueResult)
        {
            _values.Add(valueResult);

            if (Matching)
            {
                Matching = valueResult.Matching;
            }
        }

        public void ThrowOnFailure()
        {
            if (Matching)
            {
                return;
            }

            throw new DeepComparisonFailureException(GetFailingComparisons());
        }

        public int TotalComparisons()
        {
            int total = Objects.Sum(o => o.TotalComparisons());

            total += Values.Length;

            return total;
        }

        public int TotalMatchingComparisons()
        {
            int total = Objects.Sum(o => o.TotalMatchingComparisons());

            total += Values.Count(v => v.Matching);

            return total;
        }

        public int TotalNotMatchingComparisons()
        {
            int total = Objects.Sum(o => o.TotalNotMatchingComparisons());

            total += Values.Count(v => !v.Matching);

            return total;
        }
    }
}