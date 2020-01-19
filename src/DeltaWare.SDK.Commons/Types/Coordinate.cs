
using System.Diagnostics;

namespace DeltaWare.SDK.Common.Types
{
/// <summary>
    /// X:Y Coordinates.
    /// </summary>
    [DebuggerDisplay("(X: {X}; Y: {Y})")]
    public struct Coordinate
    {
        /// <summary>
        /// An <see cref="int"/> value representing the X coordinate.
        /// </summary>
        public readonly int X;

        /// <summary>
        /// An <see cref="int"/> value representing the Y coordinate.
        /// </summary>
        public readonly int Y;

        public static Coordinate Zero => new Coordinate(0, 0);

        public static Coordinate One => new Coordinate(1, 1);

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate"/> class.
        /// </summary>
        /// <param name="x">The X Coordinate.</param>
        /// <param name="y">The Y Coordinate.</param>
        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coordinate Add(Coordinate coordinate)
        {
            return new Coordinate(X + coordinate.X, Y + coordinate.Y);
        }

        public Coordinate Add(int x, int y)
        {
            return new Coordinate(X + x, Y + y);
        }

        public Coordinate AddToX(Coordinate coordinate)
        {
            return new Coordinate(X + coordinate.X, Y);
        }

        public Coordinate AddToX(int x)
        {
            return new Coordinate(X + x, Y);
        }

        public Coordinate AddToY(Coordinate coordinate)
        {
            return new Coordinate(X, Y + coordinate.Y);
        }

        public Coordinate AddToY(int y)
        {
            return new Coordinate(X, Y + y);
        }

        public Coordinate AdvanceX(int x = 1)
        {
            return new Coordinate(X + x, Y);
        }

        public Coordinate AdvanceY(int y = 1)
        {
            return new Coordinate(X, Y + y);
        }

        public Coordinate SetX(Coordinate coordinate)
        {
            return new Coordinate(coordinate.X, Y);
        }

        public Coordinate SetX(int x)
        {
            return new Coordinate(x, Y);
        }

        public Coordinate SetY(Coordinate coordinate)
        {
            return new Coordinate(X, coordinate.Y);
        }

        public Coordinate SetY(int y)
        {
            return new Coordinate(X, y);
        }

        /// <summary>
        /// Compares two <see cref="Coordinate"/> and returns a <see cref="bool"/> value specifying if they are equal.
        /// </summary>
        /// <param name="coordinateA">The first <see cref="Coordinate"/> to be compared.</param>
        /// <param name="coordinateB">the Second <see cref="Coordinate"/> to be compared.</param>
        /// <returns>A <see cref="bool"/> value specifying the outcome of the operation.</returns>
        public static bool operator ==(Coordinate coordinateA, Coordinate coordinateB)
        {
            return coordinateA.X == coordinateB.X && coordinateA.Y == coordinateB.Y;
        }

        /// <summary>
        /// Compares two <see cref="Coordinate"/> and returns a <see cref="bool"/> value specifying if they are not equal.
        /// </summary>
        /// <param name="coordinateA">The first <see cref="Coordinate"/> to be compared.</param>
        /// <param name="coordinateB">the Second <see cref="Coordinate"/> to be compared.</param>
        /// <returns>A <see cref="bool"/> value specifying the outcome of the operation.</returns>
        public static bool operator !=(Coordinate coordinateA, Coordinate coordinateB)
        {
            return !(coordinateA == coordinateB);
        }

        /// <summary>
        /// Compares two <see cref="Coordinate"/> and returns a <see cref="bool"/> value specifying if a is larger than b.
        /// </summary>
        /// <param name="coordinateA">The first <see cref="Coordinate"/> to be compared.</param>
        /// <param name="coordinateB">the Second <see cref="Coordinate"/> to be compared.</param>
        /// <returns>A <see cref="bool"/> value specifying the outcome of the operation.</returns>
        public static bool operator >(Coordinate coordinateA, Coordinate coordinateB)
        {
            return coordinateA.X + coordinateA.Y > coordinateB.X + coordinateB.Y;
        }

        /// <summary>
        /// Compares two <see cref="Coordinate"/> and returns a <see cref="bool"/> value specifying if a is less than b.
        /// </summary>
        /// <param name="coordinateA">The first <see cref="Coordinate"/> to be compared.</param>
        /// <param name="coordinateB">the Second <see cref="Coordinate"/> to be compared.</param>
        /// <returns>A <see cref="bool"/> value specifying the outcome of the operation.</returns>
        public static bool operator <(Coordinate coordinateA, Coordinate coordinateB)
        {
            return coordinateA.X + coordinateA.Y < coordinateB.X + coordinateB.Y;
        }

        /// <summary>
        /// Compares two <see cref="Coordinate"/> and returns a <see cref="bool"/> value specifying if a is larger than or equal to b.
        /// </summary>
        /// <param name="coordinateA">The first <see cref="Coordinate"/> to be compared.</param>
        /// <param name="coordinateB">the Second <see cref="Coordinate"/> to be compared.</param>
        /// <returns>A <see cref="bool"/> value specifying the outcome of the operation.</returns>
        public static bool operator >=(Coordinate coordinateA, Coordinate coordinateB)
        {
            return coordinateA.X + coordinateA.Y >= coordinateB.X + coordinateB.Y;
        }

        /// <summary>
        /// Compares two <see cref="Coordinate"/> and returns a <see cref="bool"/> value specifying if a is less than or equal to b.
        /// </summary>
        /// <param name="coordinateA">The first <see cref="Coordinate"/> to be compared.</param>
        /// <param name="coordinateB">the Second <see cref="Coordinate"/> to be compared.</param>
        /// <returns>A <see cref="bool"/> value specifying the outcome of the operation.</returns>
        public static bool operator <=(Coordinate coordinateA, Coordinate coordinateB)
        {
            return coordinateA.X + coordinateA.Y <= coordinateB.X + coordinateB.Y;
        }

        public static Coordinate operator +(Coordinate coordinateLeft, Coordinate coordinateRight)
        {
            return new Coordinate(coordinateLeft.X + coordinateRight.X, coordinateLeft.Y + coordinateRight.Y);
        }

        public static Coordinate operator -(Coordinate coordinateLeft, Coordinate coordinateRight)
        {
            return new Coordinate(coordinateLeft.X - coordinateRight.X, coordinateLeft.Y - coordinateRight.Y);
        }

        public LongCoordinate ToLongCoordinate()
        {
            return new LongCoordinate(X, Y);
        }

        /// <summary>
        /// Compares the <see cref="object"/> to the <see cref="Coordinate"/>.
        /// </summary>
        /// <param name="obj">The <see cref="object"/> to be compared to the <see cref="Coordinate"/>.</param>
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

			return this == (Coordinate)obj;

		}
    }
}
