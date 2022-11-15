using System;

namespace DeltaWare.SDK.Core.Collections.Parallel.Writer
{
    public interface IArrayWriter<in T> : IDisposable
    {
        int Length { get; }

        void Write(T value);
    }
}
