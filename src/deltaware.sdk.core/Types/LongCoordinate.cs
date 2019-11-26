
using System;
using System.Diagnostics;

namespace DeltaWare.SDK.Core.Types
{
/// <summary>
    /// X:Y Coordinates.
    /// </summary>
    [DebuggerDisplay("(X: {X}; Y: {Y})")]
    public struct LongCoordinate
    {
        /// <summary>
        /// An <see cref="long"/> value representing the X coordinate.
        /// </summary>
        public readonly long X;

        /// <summary>
        /// An <see cref="long"/> value representing the Y coordinate.
        /// </summary>
        public readonly long Y;

        public static LongCoordinate Zero => new LongCoordinate(0, 0);

        public static LongCoordinate One => new LongCoordinate(1, 1);

        /// <summary>
        /// Initializes a new instance of the <see cref="LongCoordinate"/> class.
        /// </summary>
        /// <param name="x">The X Coordinate.</param>
        /// <param name="y">The Y Coordinate.</param>
        public LongCoordinate(long x, long y)
        {
            X = x;
            Y = y;
        }

        public LongCoordinate Add(LongCoordinate coordinate)
        {
            return new LongCoordinate(X + coordinate.X, Y + coordinate.Y);
        }

        public LongCoordinate Add(long x, long y)
        {
            return new LongCoordinate(X + x, Y + y);
        }

        public LongCoordinate AddToX(LongCoordinate coordinate)
        {
            return new LongCoordinate(X + coordinate.X, Y);
        }

        public LongCoordinate AddToX(long x)
        {
            return new LongCoordinate(X + x, Y);
        }

        public LongCoordinate AddToY(LongCoordinate coordinate)
        {
            return new LongCoordinate(X, Y + coordinate.Y);
        }

        public LongCoordinate AddToY(long y)
        {
            return new LongCoordinate(X, Y + y);
        }

        public LongCoordinate AdvanceX(long x = 1)
        {
            return new LongCoordinate(X + x, Y);
        }

        public LongCoordinate AdvanceY(long y = 1)
        {
            return new LongCoordinate(X, Y + y);
        }

        public LongCoordinate SetX(LongCoordinate coordinate)
        {
            return new LongCoordinate(coordinate.X, Y);
        }

        public LongCoordinate SetX(long x)
        {
            return new LongCoordinate(x, Y);
        }

        public LongCoordinate SetY(LongCoordinate coordinate)
        {
            return new LongCoordinate(X, coordinate.Y);
        }

        public LongCoordinate SetY(long y)
        {
            return new LongCoordinate(X, y);
        }

        /// <summary>
        /// Compares two <see cref="LongCoordinate"/> and returns a <see cref="bool"/> value specifying if they are equal.
        /// </summary>
        /// <param name="coordinateA">The first <see cref="LongCoordinate"/> to be compared.</param>
        /// <param name="coordinateB">the Second <see cref="LongCoordinate"/> to be compared.</param>
        /// <returns>A <see cref="bool"/> value specifying the outcome of the operation.</returns>
        public static bool operator ==(LongCoordinate coordinateA, LongCoordinate coordinateB)
        {
            return coordinateA.X == coordinateB.X && coordinateA.Y == coordinateB.Y;
        }

        /// <summary>
        /// Compares two <see cref="LongCoordinate"/> and returns a <see cref="bool"/> value specifying if they are not equal.
        /// </summary>
        /// <param name="coordinateA">The first <see cref="LongCoordinate"/> to be compared.</param>
        /// <param name="coordinateB">the Second <see cref="LongCoordinate"/> to be compared.</param>
        /// <returns>A <see cref="bool"/> value specifying the outcome of the operation.</returns>
        public static bool operator !=(LongCoordinate coordinateA, LongCoordinate coordinateB)
        {
            return !(coordinateA == coordinateB);
        }

        /// <summary>
        /// Compares two <see cref="LongCoordinate"/> and returns a <see cref="bool"/> value specifying if a is larger than b.
        /// </summary>
        /// <param name="coordinateA">The first <see cref="LongCoordinate"/> to be compared.</param>
        /// <param name="coordinateB">the Second <see cref="LongCoordinate"/> to be compared.</param>
        /// <returns>A <see cref="bool"/> value specifying the outcome of the operation.</returns>
        public static bool operator >(LongCoordinate coordinateA, LongCoordinate coordinateB)
        {
            return coordinateA.X + coordinateA.Y > coordinateB.X + coordinateB.Y;
        }

        /// <summary>
        /// Compares two <see cref="LongCoordinate"/> and returns a <see cref="bool"/> value specifying if a is less than b.
        /// </summary>
        /// <param name="coordinateA">The first <see cref="LongCoordinate"/> to be compared.</param>
        /// <param name="coordinateB">the Second <see cref="LongCoordinate"/> to be compared.</param>
        /// <returns>A <see cref="bool"/> value specifying the outcome of the operation.</returns>
        public static bool operator <(LongCoordinate coordinateA, LongCoordinate coordinateB)
        {
            return coordinateA.X + coordinateA.Y < coordinateB.X + coordinateB.Y;
        }

        /// <summary>
        /// Compares two <see cref="LongCoordinate"/> and returns a <see cref="bool"/> value specifying if a is larger than or equal to b.
        /// </summary>
        /// <param name="coordinateA">The first <see cref="LongCoordinate"/> to be compared.</param>
        /// <param name="coordinateB">the Second <see cref="LongCoordinate"/> to be compared.</param>
        /// <returns>A <see cref="bool"/> value specifying the outcome of the operation.</returns>
        public static bool operator >=(LongCoordinate coordinateA, LongCoordinate coordinateB)
        {
            return coordinateA.X + coordinateA.Y >= coordinateB.X + coordinateB.Y;
        }

        /// <summary>
        /// Compares two <see cref="LongCoordinate"/> and returns a <see cref="bool"/> value specifying if a is less than or equal to b.
        /// </summary>
        /// <param name="coordinateA">The first <see cref="LongCoordinate"/> to be compared.</param>
        /// <param name="coordinateB">the Second <see cref="LongCoordinate"/> to be compared.</param>
        /// <returns>A <see cref="bool"/> value specifying the outcome of the operation.</returns>
        public static bool operator <=(LongCoordinate coordinateA, LongCoordinate coordinateB)
        {
            return coordinateA.X + coordinateA.Y <= coordinateB.X + coordinateB.Y;
        }

        public static LongCoordinate operator +(LongCoordinate coordinateLeft, LongCoordinate coordinateRight)
        {
            return new LongCoordinate(coordinateLeft.X + coordinateRight.X, coordinateLeft.Y + coordinateRight.Y);
        }

        public static LongCoordinate operator -(LongCoordinate coordinateLeft, LongCoordinate coordinateRight)
        {
            return new LongCoordinate(coordinateLeft.X - coordinateRight.X, coordinateLeft.Y - coordinateRight.Y);
        }

        public Coordinate ToCoordinate()
        {
            if (X > Int32.MaxValue || Y > Int32.MaxValue)
                throw new InvalidCastException();

            return new Coordinate((int)X, (int)Y);
        }


        /// <summary>
        /// Compares the <see cref="object"/> to the <see cref="LongCoordinate"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to be compared to the <see cref="LongCoordinate"/>.</param>
        /// <returns>An <see cref="int"/> value specifying if the <see cref="object"/> is equal to the <see cref="Coordinate"/>.</returns>
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

			return this == (LongCoordinate)obj;

		}
    }
}
