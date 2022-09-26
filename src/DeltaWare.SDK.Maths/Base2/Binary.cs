
using System;
using DeltaWare.SDK.Maths.Base2.Exceptions;

namespace DeltaWare.SDK.Maths.Base2
{
    public readonly struct Binary
    {
        private readonly long _value;

        public BitWidth BitWidth { get; }

        public BinaryState State { get; }

        public bool IsTwosComplementEnabled { get; }

        private Binary(long value, BitWidth bitWidth, BinaryState state, bool enableTwosCompliment)
        {
            BitWidth = bitWidth;
            State = state;
            IsTwosComplementEnabled = enableTwosCompliment;
            _value = value;
        }
        
        private Binary(int value, BitWidth bitWidth, BinaryState state, bool enableTwosCompliment)
        {
            BitWidth = bitWidth;
            State = state;
            IsTwosComplementEnabled = enableTwosCompliment;
            _value = value;
        }

        public Binary(long value, BitWidth bitWidth, bool enableTwosCompliment = false)
        {
            BitWidth = bitWidth;
            IsTwosComplementEnabled = value < 0 || enableTwosCompliment;
            _value = value;

            State = DoesValueFit(_value, BitWidth) ? BinaryState.Valid : BinaryState.Error;
        }

        public Binary(int value, BitWidth bitWidth, bool enableTwosCompliment = false)
        {
            BitWidth = bitWidth;
            IsTwosComplementEnabled = value < 0 || enableTwosCompliment;
            _value = value;

            State = DoesValueFit(_value, BitWidth) ? BinaryState.Valid : BinaryState.Error;
        }

        public Binary(BitWidth bitWidth, bool enableTwosCompliment = false)
        {
            BitWidth = bitWidth;
            IsTwosComplementEnabled = enableTwosCompliment;

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

        public static Binary Null(BitWidth bitWidth)
        {
            return new Binary(0, bitWidth, BinaryState.Null, false);
        }

        public Binary SetState(BinaryState state)
        {
            return new Binary(_value, BitWidth, state, IsTwosComplementEnabled);
        }

        public Binary MakeValid()
        {
            return SetState(BinaryState.Valid);
        }

        public Binary MakeNull()
        {
            return SetState(BinaryState.Null);
        }

        public Binary MakeError()
        {
            return SetState(BinaryState.Error);
        }

        public Binary MakeOverflow()
        {
            return SetState(BinaryState.Overflow);
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
            return TruncateBinary(valueLeft._value + valueRight._value, valueLeft.BitWidth, valueLeft.IsTwosComplementEnabled);
        }

        /// <summary>
        /// Executes a subtraction operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator -(Binary valueLeft, Binary valueRight)
        {
            return TruncateBinary(valueLeft._value - valueRight._value, valueLeft.BitWidth, valueLeft.IsTwosComplementEnabled);
        }

        /// <summary>
        /// Executes a division operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator /(Binary valueLeft, Binary valueRight)
        {
            return TruncateBinary(valueLeft._value / valueRight._value, valueLeft.BitWidth, valueLeft.IsTwosComplementEnabled);
        }

        /// <summary>
        /// Executes a multiplication operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator *(Binary valueLeft, Binary valueRight)
        {
            return TruncateBinary(valueLeft._value * valueRight._value, valueLeft.BitWidth, valueLeft.IsTwosComplementEnabled);
        }

        /// <summary>
        /// Executes a FLOW AND operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator &(Binary valueLeft, Binary valueRight)
        {
            return new Binary(valueLeft._value & valueRight._value, valueLeft.BitWidth, BinaryState.Valid, valueLeft.IsTwosComplementEnabled);
        }

        /// <summary>
        /// Executes a FLOW OR operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator |(Binary valueLeft, Binary valueRight)
        {
            return new Binary(valueLeft._value | valueRight._value, valueLeft.BitWidth, BinaryState.Valid, valueLeft.IsTwosComplementEnabled);
        }

        /// <summary>
        /// Executes a FLOW XOR operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator ^(Binary valueLeft, Binary valueRight)
        {
            return new Binary(valueLeft._value ^ valueRight._value, valueLeft.BitWidth, BinaryState.Valid, valueLeft.IsTwosComplementEnabled);
        }

        /// <summary>
        /// Executes a shift right operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The amount to shit the value.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator >>(Binary valueLeft, int valueRight)
        {
            return new Binary(valueLeft._value >> valueRight, valueLeft.BitWidth, BinaryState.Valid, valueLeft.IsTwosComplementEnabled);
        }

        /// <summary>
        /// Executes a shift left operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The amount to shit the value.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator <<(Binary valueLeft, int valueRight)
        {
            return new Binary(valueLeft._value << valueRight, valueLeft.BitWidth, BinaryState.Valid, valueLeft.IsTwosComplementEnabled);
        }

        public static Binary operator ~(Binary binary)
        {
            return new Binary(~binary._value, binary.BitWidth, BinaryState.Valid, binary.IsTwosComplementEnabled);
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
            return TruncateBinary(valueLeft._value + valueRight, valueLeft.BitWidth, valueLeft.IsTwosComplementEnabled);
        }

        /// <summary>
        /// Executes a subtraction operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator -(Binary valueLeft, long valueRight)
        {
            return TruncateBinary(valueLeft._value - valueRight, valueLeft.BitWidth, valueLeft.IsTwosComplementEnabled);
        }

        /// <summary>
        /// Executes a division operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator /(Binary valueLeft, long valueRight)
        {
            return TruncateBinary(valueLeft._value / valueRight, valueLeft.BitWidth, valueLeft.IsTwosComplementEnabled);
        }

        /// <summary>
        /// Executes a multiplication operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator *(Binary valueLeft, long valueRight)
        {
            return TruncateBinary(valueLeft._value * valueRight, valueLeft.BitWidth, valueLeft.IsTwosComplementEnabled);
        }

        /// <summary>
        /// Executes a FLOW AND operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator &(Binary valueLeft, long valueRight)
        {
            return new Binary(valueLeft._value & valueRight, valueLeft.BitWidth, BinaryState.Valid, valueLeft.IsTwosComplementEnabled);
        }

        /// <summary>
        /// Executes a FLOW OR operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator |(Binary valueLeft, long valueRight)
        {
            return new Binary(valueLeft._value | valueRight, valueLeft.BitWidth, BinaryState.Valid, valueLeft.IsTwosComplementEnabled);
        }

        /// <summary>
        /// Executes a FLOW XOR operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator ^(Binary valueLeft, long valueRight)
        {
            return new Binary(valueLeft._value ^ valueRight, valueLeft.BitWidth, BinaryState.Valid, valueLeft.IsTwosComplementEnabled);
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

        public static bool DoesValueFit(long value, BitWidth bitWidth, bool isTwosComplementEnabled = false)
        {
            if (isTwosComplementEnabled)
            {
                if (value >= 0 && value <= BitWidth.MaxSizePositive[bitWidth.ToInt()])
                {
                    return true;
                }

                return value < 0 && value >= BitWidth.MaxSizeNegative[bitWidth.ToInt()];
            }

            if (value < 0)
            {
                throw BinaryException.NegativeValueWithoutTwosCompliment();
            }

            return value <= BitWidth.MaxSizePositive[bitWidth.ToInt() + 1];
        }

        public static bool DoesValueFit(Binary binary, BitWidth bitWidth)
        {
            return DoesValueFit(binary._value, bitWidth, binary.IsTwosComplementEnabled);
        }

        public static Binary TruncateBinary(long value, BitWidth bitWidth, bool isTwosComplementEnabled = false)
        {
            if (DoesValueFit(value, bitWidth, isTwosComplementEnabled))
            {
                return new Binary(value, bitWidth, isTwosComplementEnabled);
            }

            if (isTwosComplementEnabled)
            {
                if (value >= 0)
                {
                    return new Binary(value - BitWidth.MaxSizePositive[bitWidth.ToInt()], bitWidth, BinaryState.Overflow, true);
                }

                return new Binary(value + BitWidth.MaxSizeNegative[bitWidth.ToInt()], bitWidth, BinaryState.Overflow, true);
            }

            if (value < 0)
            {
                throw BinaryException.NegativeValueWithoutTwosCompliment();
            }

            return new Binary(value - BitWidth.MaxSizePositive[bitWidth.ToInt() + 1], bitWidth, BinaryState.Overflow, false);
        }

        public static Binary TruncateBinary(Binary binary, BitWidth bitWidth)
        {
            return TruncateBinary(binary._value, bitWidth, binary.IsTwosComplementEnabled);
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
