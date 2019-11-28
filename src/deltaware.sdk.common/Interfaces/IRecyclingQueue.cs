using System;
using System.Collections.Generic;
using System.Text;
using DeltaWare.SDK.Common.Enums;

namespace DeltaWare.SDK.Common.Interfaces
{
    public interface IRecyclingQueue<T>
    {
        int Capacity { get; }

        T this[int index] { get; set; }

        void Add(T item);

        void AddMany(T[] items);

        void Clear();

        T[] GetRange(int index, int count, SortDirection sortDirection = SortDirection.Ascending);
    }
}
