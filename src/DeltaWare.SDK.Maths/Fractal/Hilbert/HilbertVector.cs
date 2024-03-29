﻿using DeltaWare.SDK.Maths.Types;

namespace DeltaWare.SDK.Maths.Fractal.Hilbert
{
    public struct HilbertVector<T>
    {
        public readonly Coordinate Coordinates;
        public readonly long Index;
        public readonly T Value;

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