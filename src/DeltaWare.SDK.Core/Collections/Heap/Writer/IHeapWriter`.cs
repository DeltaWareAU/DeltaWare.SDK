using System;

namespace DeltaWare.SDK.Core.Collections.Heap.Writer
{
    public interface IHeapWriter<in T>: IDisposable
    {
        int Length { get; }

        void Write(T value);
    }
}
