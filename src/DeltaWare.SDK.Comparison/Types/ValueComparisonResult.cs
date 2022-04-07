using System.Diagnostics;

namespace DeltaWare.SDK.Comparison.Types
{
    [DebuggerDisplay("Matching:{Matching.ToString().ToUpper()} - {Name}")]
    public class ValueComparisonResult
    {
        public bool Matching { get; }
        public string Name { get; }
        public object ValueA { get; }

        public object ValueB { get; }

        public ValueComparisonResult(string name, bool matching, object valueA, object valueB)
        {
            Name = name;
            Matching = matching;
            ValueA = valueA;
            ValueB = valueB;
        }

        public ValueComparisonResult(string name, bool matching)
        {
            Name = name;
            Matching = matching;
        }
    }
}