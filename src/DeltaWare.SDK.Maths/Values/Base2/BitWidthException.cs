using System;

namespace DeltaWare.SDK.Maths.Values.Base2
{
    public sealed class BitWidthException : Exception
    {
        public BitWidthException(string message, Exception? innerException = null) : base(message, innerException)
        {
        }

        public static BitWidthException BreachedMaximum(int value, int maxValue, Exception? innerException = null)
        {
            return new BitWidthException($"A maximum {nameof(BitWidth)} of ({maxValue}) can be used, but ({value}) was provided.", innerException);
        }

        public static BitWidthException BreachedMinimum(int value, int minValue, Exception? innerException = null)
        {
            return new BitWidthException($"A minimum {nameof(BitWidth)} of ({minValue}) can be used, but ({value}) was provided.", innerException);
        }
    }
}
