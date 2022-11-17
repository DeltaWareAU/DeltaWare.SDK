using DeltaWare.SDK.Core.Handlers;
using System.Collections.Generic;

namespace DeltaWare.SDK.Core.Helpers
{
    public static class CollectionHelper
    {
        public static ICompareCollectionHandler<T> Compare<T>(IReadOnlyCollection<T> left, IReadOnlyCollection<T> right)
            => new CompareCollectionHandler<T>(left, right);
    }
}
