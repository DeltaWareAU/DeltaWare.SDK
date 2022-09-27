using System;

namespace DeltaWare.SDK.Maths.Base2.Exceptions
{
    public class BinaryException : Exception
    {
        private BinaryException(string message, Exception? innerException = null) : base(message, innerException)
        {
        }

        public static BinaryException NegativeValueWithoutTwosCompliment()
        {
            return new BinaryException("Negative values are only support if Two's Complement is Enabled");
        }
    }
}
