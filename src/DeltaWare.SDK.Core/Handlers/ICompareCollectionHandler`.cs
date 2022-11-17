using System;

namespace DeltaWare.SDK.Core.Handlers
{
    public interface ICompareCollectionHandler<out T>
    {
        ICompareCollectionHandler<T> OnCompare(Func<T, T, bool> onMatch);

        ICompareCollectionHandler<T> ForEachRemoved(Action<T> removedItemAction);

        ICompareCollectionHandler<T> ForEachExisting(Action<T> existingItemAction);

        ICompareCollectionHandler<T> ForEachExisting(Action<T, T> existingItemAction);

        ICompareCollectionHandler<T> ForEachAdded(Action<T> addedItemAction);
    }
}
