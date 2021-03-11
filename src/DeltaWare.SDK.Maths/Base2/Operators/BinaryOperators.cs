
namespace DeltaWare.SDK.Maths.Base2
{
    public partial struct Binary
    {
        public static implicit operator long(Binary binary)
        {
            return binary._longValue;
        }

        #region Binary Operators

        public static Binary operator +(Binary valueLeft, Binary valueRight)
        {
            return valueLeft.Length.CreateBinary(valueLeft + valueRight);
        }

        public static Binary operator -(Binary valueLeft, Binary valueRight)
        {
            return valueLeft.Length.CreateBinary(valueLeft - valueRight);
        }

        public static Binary operator /(Binary valueLeft, Binary valueRight)
        {
            return valueLeft.Length.CreateBinary(valueLeft / valueRight);
        }

        public static Binary operator *(Binary valueLeft, Binary valueRight)
        {
            return valueLeft.Length.CreateBinary(valueLeft * valueRight);
        }

        public static Binary operator &(Binary valueLeft, Binary valueRight)
        {
            return new Binary(valueLeft & valueRight, valueLeft.Length);
        }

        public static Binary operator |(Binary valueLeft, Binary valueRight)
        {
            return new Binary(valueLeft | valueRight, valueLeft.Length);
        }

        public static Binary operator ^(Binary valueLeft, Binary valueRight)
        {
            return new Binary(valueLeft ^ valueRight, valueLeft.Length);
        }

        public static Binary operator >>(Binary valueLeft, int valueRight)
        {
            return new Binary(valueLeft >> valueRight, valueLeft.Length);
        }

        public static Binary operator <<(Binary valueLeft, int valueRight)
        {
            return new Binary(valueLeft << valueRight, valueLeft.Length);
        }

        public static Binary operator ~(Binary binary)
        {
            return new Binary(~binary, binary.Length);
        }

        public static Binary operator +(Binary valueLeft, long valueRight)
        {
            return valueLeft.Length.CreateBinary(valueLeft + valueRight);
        }

        public static Binary operator -(Binary valueLeft, long valueRight)
        {
            return valueLeft.Length.CreateBinary(valueLeft - valueRight);
        }

        public static Binary operator /(Binary valueLeft, long valueRight)
        {
            return valueLeft.Length.CreateBinary(valueLeft / valueRight);
        }

        public static Binary operator *(Binary valueLeft, long valueRight)
        {
            return valueLeft.Length.CreateBinary(valueLeft * valueRight);
        }

        public static Binary operator &(Binary valueLeft, long valueRight)
        {
            return new Binary(valueLeft & valueRight, valueLeft.Length);
        }

        public static Binary operator |(Binary valueLeft, long valueRight)
        {
            return new Binary(valueLeft | valueRight, valueLeft.Length);
        }

        public static Binary operator ^(Binary valueLeft, long valueRight)
        {
            return new Binary(valueLeft ^ valueRight, valueLeft.Length);
        }

        #endregion Binary Operators

        #region Boolean Operators

        public static bool operator ==(Binary valueLeft, Binary valueRight)
        {
            return valueLeft == valueRight;
        }

        public static bool operator !=(Binary valueLeft, Binary valueRight)
        {
            return valueLeft != valueRight;
        }

        public static bool operator >=(Binary valueLeft, Binary valueRight)
        {
            return valueLeft >= valueRight;
        }

        public static bool operator <=(Binary valueLeft, Binary valueRight)
        {
            return valueLeft <= valueRight;
        }

        public static bool operator >(Binary valueLeft, Binary valueRight)
        {
            return valueLeft > valueRight;
        }

        public static bool operator <(Binary valueLeft, Binary valueRight)
        {
            return valueLeft < valueRight;
        }

        public static bool operator ==(Binary valueLeft, long valueRight)
        {
            return valueLeft == valueRight;
        }

        public static bool operator !=(Binary valueLeft, long valueRight)
        {
            return valueLeft != valueRight;
        }

        public static bool operator >=(Binary valueLeft, long valueRight)
        {
            return valueLeft >= valueRight;
        }

        public static bool operator <=(Binary valueLeft, long valueRight)
        {
            return valueLeft <= valueRight;
        }

        public static bool operator >(Binary valueLeft, long valueRight)
        {
            return valueLeft > valueRight;
        }

        public static bool operator <(Binary valueLeft, long valueRight)
        {
            return valueLeft < valueRight;
        }

        #endregion Boolean Operators
    }
}
