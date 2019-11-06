
using System;
using System.Diagnostics;

namespace DeltaWare.SDK.Maths.Base2
{
    [DebuggerTypeProxy(typeof(BinaryDebugView))]
    public struct Binary
    {
        private Binary(BinaryState state, long value, BitLength bitLength)
        {
            BitLength = bitLength;
            State = state;
            Value = value;
        }

        public Binary(long value, BitLength bitLength)
        {
            BitLength = bitLength;
            State = BinaryState.Valid;
            Value = value;
        }


        public long Value { get; }

        public BitLength BitLength { get; }

        public BinaryState State { get; }

        public Binary MakeValidBinary()
        {
            return new Binary(BinaryState.Valid, Value, BitLength);
        }

        public Binary MakeUnknownBinary()
        {
            return new Binary(BinaryState.Unkown, Value, BitLength);
        }

        public Binary MakeErrorBinary()
        {
            return new Binary(BinaryState.Error, Value, BitLength);
        }

        /// <summary>
        /// Executes a addition operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator +(Binary valueLeft, Binary valueRight)
        {
            return BitLength.TruncateBinary(valueLeft.Value + valueRight.Value, valueLeft.BitLength);
        }

        /// <summary>
        /// Executes a subtraction operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator -(Binary valueLeft, Binary valueRight)
        {
            return BitLength.TruncateBinary(valueLeft.Value - valueRight.Value, valueLeft.BitLength);
        }

        /// <summary>
        /// Executes a division operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator /(Binary valueLeft, Binary valueRight)
        {
            return BitLength.TruncateBinary(valueLeft.Value / valueRight.Value, valueLeft.BitLength);
        }

        /// <summary>
        /// Executes a multiplication operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator *(Binary valueLeft, Binary valueRight)
        {
            return BitLength.TruncateBinary(valueLeft.Value * valueRight.Value, valueLeft.BitLength);
        }

        /// <summary>
        /// Executes a FLOW AND operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator &(Binary valueLeft, Binary valueRight)
        {
            return new Binary(BinaryState.Valid, valueLeft.Value & valueRight.Value, valueLeft.BitLength);
        }

        /// <summary>
        /// Executes a FLOW OR operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator |(Binary valueLeft, Binary valueRight)
        {
            return new Binary(BinaryState.Valid, valueLeft.Value | valueRight.Value, valueLeft.BitLength);
        }

        /// <summary>
        /// Executes a FLOW XOR operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="Binary"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator ^(Binary valueLeft, Binary valueRight)
        {
            return new Binary(BinaryState.Valid, valueLeft.Value ^ valueRight.Value, valueLeft.BitLength);
        }

        /// <summary>
        /// Executes a shift right operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The amount to shit the value.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator >>(Binary valueLeft, int valueRight)
        {
            return new Binary(BinaryState.Valid, valueLeft.Value >> valueRight, valueLeft.BitLength);
        }

        /// <summary>
        /// Executes a shift left operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The amount to shit the value.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator <<(Binary valueLeft, int valueRight)
        {
            return new Binary(BinaryState.Valid, valueLeft.Value << valueRight, valueLeft.BitLength);
        }

        public static Binary operator ~(Binary binary)
        {
            return new Binary(BinaryState.Valid, ~binary.Value, binary.BitLength);
        }

        public static bool operator ==(Binary valueLeft, Binary valueRight)
        {
            return valueLeft.Value == valueRight.Value;
        }

        public static bool operator !=(Binary valueLeft, Binary valueRight)
        {
            return valueLeft.Value != valueRight.Value;
        }

        public static bool operator >=(Binary valueLeft, Binary valueRight)
        {
            return valueLeft.Value >= valueRight.Value;
        }

        public static bool operator <=(Binary valueLeft, Binary valueRight)
        {
            return valueLeft.Value <= valueRight.Value;
        }

        public static bool operator >(Binary valueLeft, Binary valueRight)
        {
            return valueLeft.Value > valueRight.Value;
        }

        public static bool operator <(Binary valueLeft, Binary valueRight)
        {
            return valueLeft.Value < valueRight.Value;
        }

        /// <summary>
        /// Executes a addition operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator +(Binary valueLeft, long valueRight)
        {
            return BitLength.TruncateBinary(valueLeft.Value + valueRight, valueLeft.BitLength);
        }

        /// <summary>
        /// Executes a subtraction operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator -(Binary valueLeft, long valueRight)
        {
            return BitLength.TruncateBinary(valueLeft.Value - valueRight, valueLeft.BitLength);
        }

        /// <summary>
        /// Executes a division operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator /(Binary valueLeft, long valueRight)
        {
            return BitLength.TruncateBinary(valueLeft.Value / valueRight, valueLeft.BitLength);
        }

        /// <summary>
        /// Executes a multiplication operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator *(Binary valueLeft, long valueRight)
        {
            return BitLength.TruncateBinary(valueLeft.Value * valueRight, valueLeft.BitLength);
        }

        /// <summary>
        /// Executes a FLOW AND operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator &(Binary valueLeft, long valueRight)
        {
            return new Binary(BinaryState.Valid, valueLeft.Value & valueRight, valueLeft.BitLength);
        }

        /// <summary>
        /// Executes a FLOW OR operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator |(Binary valueLeft, long valueRight)
        {
            return new Binary(BinaryState.Valid, valueLeft.Value | valueRight, valueLeft.BitLength);
        }

        /// <summary>
        /// Executes a FLOW XOR operation on two <see cref="Binary"/> objects.
        /// </summary>
        /// <param name="valueLeft">The first <see cref="Binary"/> to have the operation run on.</param>
        /// <param name="valueRight">The second <see cref="long"/> to have the operation run on.</param>
        /// <returns>A new <see cref="Binary"/> containing the results of the operation.</returns>
        public static Binary operator ^(Binary valueLeft, long valueRight)
        {
            return new Binary(BinaryState.Valid, valueLeft.Value ^ valueRight, valueLeft.BitLength);
        }

        public static bool operator ==(Binary valueLeft, long valueRight)
        {
            return valueLeft.Value == valueRight;
        }

        public static bool operator !=(Binary valueLeft, long valueRight)
        {
            return valueLeft.Value != valueRight;
        }

        public static bool operator >=(Binary valueLeft, long valueRight)
        {
            return valueLeft.Value >= valueRight;
        }

        public static bool operator <=(Binary valueLeft, long valueRight)
        {
            return valueLeft.Value <= valueRight;
        }

        public static bool operator >(Binary valueLeft, long valueRight)
        {
            return valueLeft.Value > valueRight;
        }

        public static bool operator <(Binary valueLeft, long valueRight)
        {
            return valueLeft.Value < valueRight;
        }

        public new string ToString()
        {
            string value = Convert.ToString(Value, 2);

            if (value.Length > BitLength)
            {
                value = value.Substring(BitLength.Length);
            }
            else if (value.Length < BitLength)
            {
                value = new string('0', BitLength.Length - value.Length) + value;
            }

            return value;
        }

        public long ToInt64()
        {
            return Value;
        }
    }
}
