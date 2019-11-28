using System;
using System.Collections.Generic;
using System.Text;

namespace DeltaWare.SDK.Core.Interfaces
{
    public interface IRecyclingQueue<T>
    {
        int Capacity { get; }

        T this[int index] { get; set; }

        void Add(T item);

        void AddMany(T[] items);

        void Clear();

        T[] GetRange(int index, int count);
    }
}
