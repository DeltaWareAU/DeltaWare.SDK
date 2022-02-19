// ReSharper disable once CheckNamespace
namespace System
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