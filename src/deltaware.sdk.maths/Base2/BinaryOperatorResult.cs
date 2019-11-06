
namespace DeltaWare.SDK.Maths.Base2
{
    public struct BinaryOperatorResult
    {
        public BinaryOperatorResult(Binary result, Binary overflow)
        {
            Result = result;
            Overflow = overflow;
        }

        public Binary Result { get; }

        public Binary Overflow { get; }

        public static BinaryOperatorResult operator +(BinaryOperatorResult valueLeft, Binary valueRight)
        {
            Binary binaryResult = new Binary(valueLeft.Result.Value + valueRight.Value, valueLeft.Result.BitLength);

            if (binaryResult.Value >= valueLeft.Result.Value && binaryResult.Value >= valueRight.Value)
                return new BinaryOperatorResult(binaryResult, valueLeft.Overflow);

            Binary binaryOverflow = new Binary(valueLeft.Overflow.Value + 1, valueLeft.Overflow.BitLength);

            return new BinaryOperatorResult(binaryResult, binaryOverflow);

        }
    }
}
