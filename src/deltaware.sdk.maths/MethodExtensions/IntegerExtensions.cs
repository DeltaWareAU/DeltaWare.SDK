namespace DeltaWare.SDK.Maths.MethodExtensions
{
    public static class IntegerExtensions
    {
        public static bool IsEven(this int value)
        {
            return value % 2 == 0;
        }

        public static bool IsOdd(this int value)
        {
            return !IsEven(value);
        }
    }
}
