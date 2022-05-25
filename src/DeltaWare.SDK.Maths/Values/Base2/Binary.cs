
using System;

namespace DeltaWare.SDK.Maths.Values.Base2
{
    public readonly struct Binary
    {
        private readonly long _value;

        public BitWidth BitWidth { get; }

        public BinaryState State { get; }

        private Binary(long value, BitWidth bitWidth, BinaryState state)
        {
            BitWidth = bitWidth;
            State = state;
            _value = value;
        }

        public Binary(long value, BitWidth bitWidth)
        {
            BitWidth = bitWidth;
            _value = value;

            State = DoesValueFit(_value, BitWidth) ? BinaryState.Valid : BinaryState.Error;
        }

        public Binary(BitWidth bitWidth)
        {
            BitWidth = bitWidth;

            _value = 0;

            State = BinaryState.Null;
        }

        public static Binary True()
        {
            return new Binary(1, BitWidth.One);
        }

        public static Binary False()
        {
            return new Binary(0, BitWidth.One);
        }

        public Binary MakeValid()
        {
            return new Binary(_value, BitWidth, BinaryState.Valid);
        }

        public Binary MakeNull()
        {
            return new Binary(_value, BitWidth, BinaryState.Null);
        }

        public static Binary Null(BitWidth bitWidth)
        {
            return new Binary(0, bitWidth, BinaryState.Null);
        }

        public Binary MakeError()
        {
            return new Binary(_value, BitWidth, BinaryState.Error);
        }

        #region Operators

        /// <summary>
        /// Executes a addition operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator +(Binary valueLeft, Binary valueRight)
        {
            return TruncateBinary(valueLeft._value + valueRight._value, valueLeft.BitWidth);
        }

        /// <summary>
        /// Executes a subtraction operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator -(Binary valueLeft, Binary valueRight)
        {
            return TruncateBinary(valueLeft._value - valueRight._value, valueLeft.BitWidth);
        }

        /// <summary>
        /// Executes a division operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator /(Binary valueLeft, Binary valueRight)
        {
            return TruncateBinary(valueLeft._value / valueRight._value, valueLeft.BitWidth);
        }

        /// <summary>
        /// Executes a multiplication operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator *(Binary valueLeft, Binary valueRight)
        {
            return TruncateBinary(valueLeft._value * valueRight._value, valueLeft.BitWidth);
        }

        /// <summary>
        /// Executes a FLOW AND operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator &(Binary valueLeft, Binary valueRight)
        {
            return new Binary(valueLeft._value & valueRight._value, valueLeft.BitWidth, BinaryState.Valid);
        }

        /// <summary>
        /// Executes a FLOW OR operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator |(Binary valueLeft, Binary valueRight)
        {
            return new Binary(valueLeft._value | valueRight._value, valueLeft.BitWidth, BinaryState.Valid);
        }

        /// <summary>
        /// Executes a FLOW XOR operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator ^(Binary valueLeft, Binary valueRight)
        {
            return new Binary(valueLeft._value ^ valueRight._value, valueLeft.BitWidth, BinaryState.Valid);
        }

        /// <summary>
        /// Executes a shift right operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The amount to shit the value.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator >>(Binary valueLeft, int valueRight)
        {
            return new Binary(valueLeft._value >> valueRight, valueLeft.BitWidth, BinaryState.Valid);
        }

        /// <summary>
        /// Executes a shift left operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The amount to shit the value.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator <<(Binary valueLeft, int valueRight)
        {
            return new Binary(valueLeft._value << valueRight, valueLeft.BitWidth, BinaryState.Valid);
        }

        public static Binary operator ~(Binary binary)
        {
            return new Binary(~binary._value, binary.BitWidth, BinaryState.Valid);
        }

        public static bool operator ==(Binary valueLeft, Binary valueRight)
        {
            return valueLeft._value == valueRight._value;
        }

        public static bool operator !=(Binary valueLeft, Binary valueRight)
        {
            return valueLeft._value != valueRight._value;
        }

        public static bool operator >=(Binary valueLeft, Binary valueRight)
        {
            return valueLeft._value >= valueRight._value;
        }

        public static bool operator <=(Binary valueLeft, Binary valueRight)
        {
            return valueLeft._value <= valueRight._value;
        }

        public static bool operator >(Binary valueLeft, Binary valueRight)
        {
            return valueLeft._value > valueRight._value;
        }

        public static bool operator <(Binary valueLeft, Binary valueRight)
        {
            return valueLeft._value < valueRight._value;
        }

        /// <summary>
        /// Executes a addition operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator +(Binary valueLeft, long valueRight)
        {
            return TruncateBinary(valueLeft._value + valueRight, valueLeft.BitWidth);
        }

        /// <summary>
        /// Executes a subtraction operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator -(Binary valueLeft, long valueRight)
        {
            return TruncateBinary(valueLeft._value - valueRight, valueLeft.BitWidth);
        }

        /// <summary>
        /// Executes a division operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator /(Binary valueLeft, long valueRight)
        {
            return TruncateBinary(valueLeft._value / valueRight, valueLeft.BitWidth);
        }

        /// <summary>
        /// Executes a multiplication operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator *(Binary valueLeft, long valueRight)
        {
            return TruncateBinary(valueLeft._value * valueRight, valueLeft.BitWidth);
        }

        /// <summary>
        /// Executes a FLOW AND operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator &(Binary valueLeft, long valueRight)
        {
            return new Binary(valueLeft._value & valueRight, valueLeft.BitWidth, BinaryState.Valid);
        }

        /// <summary>
        /// Executes a FLOW OR operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator |(Binary valueLeft, long valueRight)
        {
            return new Binary(valueLeft._value | valueRight, valueLeft.BitWidth, BinaryState.Valid);
        }

        /// <summary>
        /// Executes a FLOW XOR operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator ^(Binary valueLeft, long valueRight)
        {
            return new Binary(valueLeft._value ^ valueRight, valueLeft.BitWidth, BinaryState.Valid);
        }

        public static bool operator ==(Binary valueLeft, long valueRight)
        {
            return valueLeft._value == valueRight;
        }

        public static bool operator !=(Binary valueLeft, long valueRight)
        {
            return valueLeft._value != valueRight;
        }

        public static bool operator >=(Binary valueLeft, long valueRight)
        {
            return valueLeft._value >= valueRight;
        }

        public static bool operator <=(Binary valueLeft, long valueRight)
        {
            return valueLeft._value <= valueRight;
        }

        public static bool operator >(Binary valueLeft, long valueRight)
        {
            return valueLeft._value > valueRight;
        }

        public static bool operator <(Binary valueLeft, long valueRight)
        {
            return valueLeft._value < valueRight;
        }

        #endregion

        #region Static Methods

        public static bool DoesValueFit(long value, BitWidth bitWidth)
        {
            if (value >= 0 && value <= BitWidth.MaxSizePositive[bitWidth.ToInt()])
            {
                return true;
            }

            return value >= BitWidth.MaxSizeNegative[bitWidth.ToInt()];
        }

        public static bool DoesValueFit(Binary binary, BitWidth bitWidth)
        {
            if (binary >= 0 && binary <= BitWidth.MaxSizePositive[bitWidth.ToInt()])
            {
                return true;
            }

            return binary >= BitWidth.MaxSizeNegative[bitWidth.ToInt()];
        }

        public static Binary TruncateBinary(long value, BitWidth bitWidth)
        {
            if (DoesValueFit(value, bitWidth))
            {
                return new Binary(value, bitWidth);
            }

            if (value >= 0)
            {
                return new Binary(value - BitWidth.MaxSizePositive[bitWidth.ToInt()], bitWidth);
            }

            return new Binary(value + BitWidth.MaxSizeNegative[bitWidth.ToInt()], bitWidth);
        }

        public static Binary TruncateBinary(Binary binary, BitWidth bitWidth)
        {
            if (DoesValueFit(binary, bitWidth))
            {
                return binary;
            }

            if (binary >= 0)
            {
                return binary - BitWidth.MaxSizePositive[bitWidth.ToInt()];
            }

            return binary + BitWidth.MaxSizeNegative[bitWidth.ToInt()];
        }

        #endregion

        #region Supporting Methods

        public bool Equals(Binary other)
        {
            return _value == other._value && BitWidth.Equals(other.BitWidth) && State == other.State;
        }

        public override bool Equals(object? obj)
        {
            return obj is Binary other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value, BitWidth, (int)State);
        }

        public new string ToString()
        {
            string value = Convert.ToString(_value, 2);

            if (value.Length > BitWidth)
            {
                value = value.Substring(BitWidth.ToInt());
            }
            else if (value.Length < BitWidth)
            {
                value = new string('0', BitWidth.ToInt() - value.Length) + value;
            }

            return value;
        }

        public int ToInt()
        {
            return checked((int)_value);
        }

        public long ToInt64()
        {
            return _value;
        }

        #endregion
    }
}
