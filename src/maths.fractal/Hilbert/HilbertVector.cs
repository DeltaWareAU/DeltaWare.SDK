using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaWare.Tools.Maths.Fractal.Hilbert
{
    public struct HilbertVector<T>
    {
        public readonly T Item;

        public readonly long X;
        public readonly long Y;
        public readonly long Index;

        internal HilbertVector(T item, long x, long y, long index)
        {
            Item = item;
            X = x;
            Y = y;
            Index = index;
        }
    }
}
