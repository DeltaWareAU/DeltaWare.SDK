using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeltaWare.Tools.Maths.Fractal.Hilbert
{
    public struct HilbertVector<T>
    {
        public readonly T Value;

        public readonly int X;
        public readonly int Y;
        public readonly long Index;

        internal HilbertVector(T value, int x, int y, long index)
        {
            Value = value;
            X = x;
            Y = y;
            Index = index;
        }
    }
}
