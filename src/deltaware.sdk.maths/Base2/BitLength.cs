﻿
namespace DeltaWare.SDK.Maths.Base2
{
    public class BitLength
    {
        private static readonly long[] MaxSizePositive =
        {
            0, 0, 1, 3, 7, 15, 31, 63, 127, 255, 511, 1023, 2048, 4095, 8191, 16383, 32767, 65535, 131071, 262143,
            524287, 1048575, 2097151, 4194303, 8388607, 16777215, 33554431, 67108863, 134217727, 268435455,
            536870911, 1073741823, 2147483647, 4294967295, 8589934591, 17179869183, 34359738367, 68719476735,
            137438953471, 274877906943, 549755813887, 1099511627775, 2199023255551, 4398046511103, 8796093022207,
            17592186044415, 35184372088831, 70368744177663, 140737488355327, 281474976710655, 562949953421311,
            1125899906842619, 2251799813685247, 4503599627370495, 9007199254740991, 18014398509481983,
            36028797018963967, 72057594037927935, 144115188075855871, 288230376151711743, 576460752303423487,
            1152921504606846975, 2305843009213693951, 4611686018427387903, 9223372036854775807
        };

        private static readonly long[] MaxSizeNegative =
        {
            0, 0, -2, -4, -8, -16, -32, -64, -128, -256, -512, -1024, -2048, -4096, -8192, -16384, -32768, -65536,
            -131072, -262144, 524288, -1048576, -2097152, -4194304, -8388608, -16777216, -33554432, -67108864,
            -134217728, -268435456, 536870912, -1073741824, -2147483648, -4294967296, -8589934592, -17179869184,
            -34359738368, -68719476736, 137438953472, -274877906944, -549755813888, -1099511627776, -2199023255552,
            -4398046511104, -8796093022208, 17592186044416, -35184372088832, -70368744177664, -140737488355328,
            -281474976710656, -562949953421312, 1125899906842620, -2251799813685248, -4503599627370496,
            -9007199254740992, -18014398509481984, 36028797018963968, -72057594037927936, -144115188075855872,
            -288230376151711744, -576460752303423488, 1152921504606846976, -2305843009213693952,
            -4611686018427387904, -9223372036854775808
        };

        public BitLength(int length)
        {
            Length = length;
        }

        public int Length { get; }

        public static bool operator ==(BitLength valueLeft, BitLength valueRight)
        {
            return valueLeft.Length == valueRight.Length;
        }

        public static bool operator !=(BitLength valueLeft, BitLength valueRight)
        {
            return valueLeft.Length != valueRight.Length;
        }

        public static bool operator >(BitLength valueLeft, BitLength valueRight)
        {
            return valueLeft.Length > valueRight.Length;
        }

        public static bool operator <(BitLength valueLeft, BitLength valueRight)
        {
            return valueLeft.Length < valueRight.Length;
        }

        public static bool operator >=(BitLength valueLeft, BitLength valueRight)
        {
            return valueLeft.Length >= valueRight.Length;
        }

        public static bool operator <=(BitLength valueLeft, BitLength valueRight)
        {
            return valueLeft.Length <= valueRight.Length;
        }

        public static bool operator ==(BitLength valueLeft, int valueRight)
        {
            return valueLeft.Length == valueRight;
        }

        public static bool operator !=(BitLength valueLeft, int valueRight)
        {
            return valueLeft.Length != valueRight;
        }

        public static bool operator >(BitLength valueLeft, int valueRight)
        {
            return valueLeft.Length > valueRight;
        }

        public static bool operator <(BitLength valueLeft, int valueRight)
        {
            return valueLeft.Length < valueRight;
        }

        public static bool operator >=(BitLength valueLeft, int valueRight)
        {
            return valueLeft.Length >= valueRight;
        }

        public static bool operator <=(BitLength valueLeft, int valueRight)
        {
            return valueLeft.Length <= valueRight;
        }

        public static bool operator ==(int valueLeft, BitLength valueRight)
        {
            return valueLeft == valueRight.Length;
        }

        public static bool operator !=(int valueLeft, BitLength valueRight)
        {
            return valueLeft != valueRight.Length;
        }

        public static bool operator >(int valueLeft, BitLength valueRight)
        {
            return valueLeft > valueRight.Length;
        }

        public static bool operator <(int valueLeft, BitLength valueRight)
        {
            return valueLeft < valueRight.Length;
        }

        public static bool operator >=(int valueLeft, BitLength valueRight)
        {
            return valueLeft >= valueRight.Length;
        }

        public static bool operator <=(int valueLeft, BitLength valueRight)
        {
            return valueLeft <= valueRight.Length;
        }

        public static bool DoesValueFit(long value, BitLength bitLength)
        {
            if (value >= 0 && value <= MaxSizePositive[bitLength.Length])
            {
                return true;
            }

            return value >= MaxSizeNegative[bitLength.Length];
        }

        public static bool DoesValueFit(Binary binary, BitLength bitLength)
        {
            if (binary >= 0 && binary <= MaxSizePositive[bitLength.Length])
            {
                return true;
            }

            return binary >= MaxSizeNegative[bitLength.Length];
        }

        public static Binary TruncateBinary(long value, BitLength bitLength)
        {
            if (DoesValueFit(value, bitLength))
            {
                return new Binary(value, bitLength);
            }

            if (value >= 0)
            {
                return new Binary(value - MaxSizePositive[bitLength.Length], bitLength);
            }

            return new Binary(value + MaxSizeNegative[bitLength.Length], bitLength);
        }

        public static Binary TruncateBinary(Binary binary, BitLength bitLength)
        {
            if (DoesValueFit(binary, bitLength))
            {
                return binary;
            }

            if (binary >= 0)
            {
                return binary - MaxSizePositive[bitLength.Length];
            }

            return binary + MaxSizeNegative[bitLength.Length];
        }

        public override string ToString()
        {
            return Length.ToString();
        }

        public int ToInt()
        {
            return Length;
        }
    }
}
