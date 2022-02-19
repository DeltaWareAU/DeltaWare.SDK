namespace DeltaWare.SDK.Core.Collections.RecyclingQueue
{
    public interface IRecyclingQueue<T>
    {
        int Capacity { get; }

        int Count { get; }

        T this[int index] { get; set; }

        void Add(T item);

        void AddMany(T[] items);

        void Clear();

        IRecyclingQueue<T> Expand(int newCapacity);

        T[] GetRange(int index, int count, SortDirection direction = SortDirection.Ascending);
    }
}