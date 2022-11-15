using System;
using System.Collections.Generic;

namespace DeltaWare.SDK.Core.Collections.Parallel.Reader
{
    public interface IArrayReader<T> : IDisposable, IEnumerable<T>
    {
        int Length { get; }

        int Position { get; }

        bool TryRead(out T? value);
    }
}
