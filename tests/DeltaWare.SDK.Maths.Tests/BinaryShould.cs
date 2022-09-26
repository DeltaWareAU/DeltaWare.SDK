using DeltaWare.SDK.Maths.Base2;
using Shouldly;
using Xunit;

namespace DeltaWare.SDK.Maths.Tests
{
    public class BinaryShould
    {
        [Theory]
        [InlineData(1, 1, 8, 2, BinaryState.Valid)]
        [InlineData(2, 2, 8, 4, BinaryState.Valid)]
        [InlineData(2, 5, 8, 7, BinaryState.Valid)]
        [InlineData(15, 1, 4, 1, BinaryState.Overflow)]
        public void Add(int valueA, int valueB, int bitWidth, int expectedValue, BinaryState expectedState)
        {
            Binary binaryA = new Binary(valueA, BitWidth.FromInt(bitWidth));
            Binary binaryB = new Binary(valueB, BitWidth.FromInt(bitWidth));

            Binary binaryC = binaryA + binaryB;

            binaryC.BitWidth.ToInt().ShouldBe(bitWidth);
            binaryC.ToInt().ShouldBe(expectedValue);
            binaryC.State.ShouldBe(expectedState);
        }
    }
}