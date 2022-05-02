using DeltaWare.SDK.Comparison.Types;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Comparison
{
    public interface IDeepObjectComparer
    {
        ObjectComparisonResults Compare(object comparisonA, object comparisonB, string name = null);

        Task<ObjectComparisonResults> CompareAsync(object comparisonA, object comparisonB, string name = null);

        ObjectComparisonResults CompareMany(IEnumerable<object> comparisonA, IEnumerable<object> comparisonB, string name = null);

        Task<ObjectComparisonResults> CompareManyAsync(IEnumerable<object> comparisonA, IEnumerable<object> comparisonB, string name = null);
    }
}