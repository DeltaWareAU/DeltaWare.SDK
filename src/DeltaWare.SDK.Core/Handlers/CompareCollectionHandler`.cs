using System;
using System.Collections.Generic;
using System.Linq;

namespace DeltaWare.SDK.Core.Handlers
{
    internal sealed class CompareCollectionHandler<T> : ICompareCollectionHandler<T>
    {
        private static readonly Func<T, T, bool> DefaultOnCompare = (left, right) => left.Equals(right);

        private readonly IReadOnlyCollection<T> _sourceLeft;
        private readonly IReadOnlyCollection<T> _sourceRight;

        private Func<T, T, bool> _onCompare = DefaultOnCompare;

        private bool _hasCompared;

        private ICollection<T> _removedItems;
        private ICollection<Tuple<T, T>> _existingItems;
        private ICollection<T> _addedItems;

        public CompareCollectionHandler(IReadOnlyCollection<T> sourceLeft, IReadOnlyCollection<T> sourceRight)
        {
            _sourceLeft = sourceLeft;
            _sourceRight = sourceRight;
        }

        public ICompareCollectionHandler<T> OnCompare(Func<T, T, bool> onMatch)
        {
            if (!_hasCompared)
            {
                _onCompare = onMatch;
            }

            return this;
        }

        private void Compare()
        {
            if (_hasCompared)
            {
                return;
            }

            _hasCompared = true;

            _removedItems = _sourceRight.Where(c => _sourceLeft.All(p => !_onCompare(p, c))).ToArray();
            _addedItems = _sourceLeft.Where(c => _sourceRight.All(p => !_onCompare(p, c))).ToArray();

            List<Tuple<T, T>> existing = new List<Tuple<T, T>>();

            foreach (T l in _sourceLeft)
            {
                T? r = _sourceRight.SingleOrDefault(r => _onCompare(r, l));

                if (r == null)
                {
                    continue;
                }

                existing.Add(new Tuple<T, T>(l, r));
            }

            _existingItems = existing;
        }

        public ICompareCollectionHandler<T> ForEachAdded(Action<T> addedItemAction)
        {
            Compare();

            foreach (T addedItem in _addedItems)
            {
                addedItemAction.Invoke(addedItem);
            }

            return this;
        }

        public ICompareCollectionHandler<T> ForEachExisting(Action<T> existingItemAction)
        {
            Compare();

            foreach (Tuple<T, T> existingItem in _existingItems)
            {
                existingItemAction.Invoke(existingItem.Item1);
            }

            return this;
        }

        public ICompareCollectionHandler<T> ForEachExisting(Action<T, T> existingItemAction)
        {
            Compare();

            foreach (Tuple<T, T> existingItem in _existingItems)
            {
                existingItemAction.Invoke(existingItem.Item1, existingItem.Item2);
            }

            return this;
        }

        public ICompareCollectionHandler<T> ForEachRemoved(Action<T> removedItemAction)
        {
            Compare();

            foreach (T removedItem in _removedItems)
            {
                removedItemAction.Invoke(removedItem);
            }

            return this;
        }
    }
}
