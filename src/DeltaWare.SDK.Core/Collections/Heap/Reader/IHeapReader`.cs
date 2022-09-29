using System;
using System.Collections.Generic;

namespace DeltaWare.SDK.Core.Collections.Heap.Reader
{
    public interface IHeapReader<T>: IDisposable, IEnumerable<T>
    {
        int Length { get; }

        int Position { get; }

        bool TryRead(out T? value);
    }
}
