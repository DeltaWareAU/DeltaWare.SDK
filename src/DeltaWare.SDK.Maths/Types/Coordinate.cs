using System;
using System.Diagnostics;

namespace DeltaWare.SDK.Maths.Types
{
    /// <summary>
    /// X:Y Coordinates.
    /// </summary>
    [DebuggerDisplay("(X: {X}; Y: {Y})")]
    public readonly struct Coordinate
    {
        public bool Equals(Coordinate other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        /// <summary>
        /// An <see cref="long"/> value representing the X coordinate.
        /// </summary>
        public readonly long X;

        /// <summary>
        /// An <see cref="long"/> value representing the Y coordinate.
        /// </summary>
        public readonly long Y;

        public static Coordinate One => new Coordinate(1, 1);
        public static Coordinate Zero => new Coordinate(0, 0);

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate"/> class.
        /// </summary>
        /// <param name="x">The X Coordinate.</param>
        /// <param name="y">The Y Coordinate.</param>
        public Coordinate(long x, long y)
        {
            X = x;
            Y = y;
        }

        public static Coordinate operator -(Coordinate coordinateLeft, Coordinate coordinateRight)
        {
            return new Coordinate(coordinateLeft.X - coordinateRight.X, coordinateLeft.Y - coordinateRight.Y);
        }

        /// <summary>
        /// Compares two <see cref="Coordinate"/> and returns a <see cref="bool"/> value
        /// specifying if they are not equal.
        /// </summary>
        /// <param name="coordinateA">The first <see cref="Coordinate"/> to be compared.</param>
        /// <param name="coordinateB">the Second <see cref="Coordinate"/> to be compared.</param>
        /// <returns>A <see cref="bool"/> value specifying the outcome of the operation.</returns>
        public static bool operator !=(Coordinate coordinateA, Coordinate coordinateB)
        {
            return !(coordinateA == coordinateB);
        }

        public static Coordinate operator +(Coordinate coordinateLeft, Coordinate coordinateRight)
        {
            return new Coordinate(coordinateLeft.X + coordinateRight.X, coordinateLeft.Y + coordinateRight.Y);
        }

        /// <summary>
        /// Compares two <see cref="Coordinate"/> and returns a <see cref="bool"/> value
        /// specifying if a is less than b.
        /// </summary>
        /// <param name="coordinateA">The first <see cref="Coordinate"/> to be compared.</param>
        /// <param name="coordinateB">the Second <see cref="Coordinate"/> to be compared.</param>
        /// <returns>A <see cref="bool"/> value specifying the outcome of the operation.</returns>
        public static bool operator <(Coordinate coordinateA, Coordinate coordinateB)
        {
            return coordinateA.X + coordinateA.Y < coordinateB.X + coordinateB.Y;
        }

        /// <summary>
        /// Compares two <see cref="Coordinate"/> and returns a <see cref="bool"/> value
        /// specifying if a is less than or equal to b.
        /// </summary>
        /// <param name="coordinateA">The first <see cref="Coordinate"/> to be compared.</param>
        /// <param name="coordinateB">the Second <see cref="Coordinate"/> to be compared.</param>
        /// <returns>A <see cref="bool"/> value specifying the outcome of the operation.</returns>
        public static bool operator <=(Coordinate coordinateA, Coordinate coordinateB)
        {
            return coordinateA.X + coordinateA.Y <= coordinateB.X + coordinateB.Y;
        }

        /// <summary>
        /// Compares two <see cref="Coordinate"/> and returns a <see cref="bool"/> value
        /// specifying if they are equal.
        /// </summary>
        /// <param name="coordinateA">The first <see cref="Coordinate"/> to be compared.</param>
        /// <param name="coordinateB">the Second <see cref="Coordinate"/> to be compared.</param>
        /// <returns>A <see cref="bool"/> value specifying the outcome of the operation.</returns>
        public static bool operator ==(Coordinate coordinateA, Coordinate coordinateB)
        {
            return coordinateA.X == coordinateB.X && coordinateA.Y == coordinateB.Y;
        }

        /// <summary>
        /// Compares two <see cref="Coordinate"/> and returns a <see cref="bool"/> value
        /// specifying if a is larger than b.
        /// </summary>
        /// <param name="coordinateA">The first <see cref="Coordinate"/> to be compared.</param>
        /// <param name="coordinateB">the Second <see cref="Coordinate"/> to be compared.</param>
        /// <returns>A <see cref="bool"/> value specifying the outcome of the operation.</returns>
        public static bool operator >(Coordinate coordinateA, Coordinate coordinateB)
        {
            return coordinateA.X + coordinateA.Y > coordinateB.X + coordinateB.Y;
        }

        /// <summary>
        /// Compares two <see cref="Coordinate"/> and returns a <see cref="bool"/> value
        /// specifying if a is larger than or equal to b.
        /// </summary>
        /// <param name="coordinateA">The first <see cref="Coordinate"/> to be compared.</param>
        /// <param name="coordinateB">the Second <see cref="Coordinate"/> to be compared.</param>
        /// <returns>A <see cref="bool"/> value specifying the outcome of the operation.</returns>
        public static bool operator >=(Coordinate coordinateA, Coordinate coordinateB)
        {
            return coordinateA.X + coordinateA.Y >= coordinateB.X + coordinateB.Y;
        }

        public Coordinate Add(Coordinate coordinate)
        {
            return new Coordinate(X + coordinate.X, Y + coordinate.Y);
        }

        public Coordinate Add(long x, long y)
        {
            return new Coordinate(X + x, Y + y);
        }

        public Coordinate AddToX(Coordinate coordinate)
        {
            return new Coordinate(X + coordinate.X, Y);
        }

        public Coordinate AddToX(long x)
        {
            return new Coordinate(X + x, Y);
        }

        public Coordinate AddToY(Coordinate coordinate)
        {
            return new Coordinate(X, Y + coordinate.Y);
        }

        public Coordinate AddToY(long y)
        {
            return new Coordinate(X, Y + y);
        }

        public Coordinate AdvanceX(long x = 1)
        {
            return new Coordinate(X + x, Y);
        }

        public Coordinate AdvanceY(long y = 1)
        {
            return new Coordinate(X, Y + y);
        }

        /// <summary>
        /// Compares the <see cref="object"/> to the <see cref="Coordinate"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to be compared to the <see cref="Coordinate"/>.</param>
        /// <returns>
        /// An <see cref="int"/> value specifying if the <see cref="object"/> is equal to the <see cref="Coordinate"/>.
        /// </returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (GetType() != obj.GetType())
            {
                return false;
            }

            return this == (Coordinate)obj;
        }

        public Coordinate SetX(Coordinate coordinate)
        {
            return new Coordinate(coordinate.X, Y);
        }

        public Coordinate SetX(long x)
        {
            return new Coordinate(x, Y);
        }

        public Coordinate SetY(Coordinate coordinate)
        {
            return new Coordinate(X, coordinate.Y);
        }

        public Coordinate SetY(long y)
        {
            return new Coordinate(X, y);
        }

        public Coordinate ToCoordinate()
        {
            if (X > Int32.MaxValue || Y > Int32.MaxValue)
                throw new InvalidCastException();

            return new Coordinate((int)X, (int)Y);
        }
    }
}