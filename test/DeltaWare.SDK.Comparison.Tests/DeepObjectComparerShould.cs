using DeltaWare.SDK.Comparison.Types;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace DeltaWare.SDK.Comparison.Tests
{
    public class DeepObjectComparerShould
    {
        [Fact]
        public void CompareManyA()
        {
            IDeepObjectComparer deepObjectComparer = new DeepObjectComparer();

            List<TestObject> comparison = new();

            comparison.Add(new TestObject());
            comparison.Add(new TestObject());
            comparison.Add(new TestObject());
            comparison.Add(new TestObject());
            comparison.Add(new TestObject());
            comparison.Add(new TestObject());
            comparison.Add(new TestObject());

            ObjectComparisonResults results = deepObjectComparer.CompareMany(comparison, comparison);

            results.Matching.ShouldBeTrue();
        }

        [Fact]
        public void CompareManyB()
        {
            IDeepObjectComparer deepObjectComparer = new DeepObjectComparer();

            List<TestObject> comparisonA = new();

            comparisonA.Add(new TestObject());
            comparisonA.Add(new TestObject());
            comparisonA.Add(new TestObject());
            comparisonA.Add(new TestObject());
            comparisonA.Add(new TestObject());
            comparisonA.Add(new TestObject());
            comparisonA.Add(new TestObject());

            List<TestObject> comparisonB = new();

            comparisonB.Add(new TestObject());
            comparisonB.Add(new TestObject());
            comparisonB.Add(new TestObject());
            comparisonB.Add(new TestObject());
            comparisonB.Add(new TestObject());
            comparisonB.Add(new TestObject());
            comparisonB.Add(new TestObject());

            ObjectComparisonResults results = deepObjectComparer.CompareMany(comparisonA, comparisonB);

            results.Matching.ShouldBeFalse();
        }

        private class TestObject
        {
            public string String { get; set; } = Guid.NewGuid().ToString();
        }
    }
}