using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeltaWare.SDK.Core;
using DeltaWare.SDK.Core.Types;

namespace DeltaWare.SDK.Maths.Fractal.Hilbert
{
    public struct HilbertVector<T>
    {
        public readonly T Value;

        public readonly Coordinate Coordinates;

        public readonly long Index;

        internal HilbertVector(T value, int x, int y, long index)
        {
            Value = value;
            Coordinates = new Coordinate(x, y);
            Index = index;
        }
        
        internal HilbertVector(T value, Coordinate coordinates, long index)
        {
            Value = value;
            Coordinates = coordinates;
            Index = index;
        }
    }
}
