using Shouldly;
using System;
using Xunit;

namespace DeltaWare.SDK.Core.Tests.Extensions
{
    public class StringExtensionsShould
    {
        [Theory]
        [InlineData("A1B2C3", "123")]
        [InlineData("ABC123", "123")]
        [InlineData("ABC 123", "123")]
        [InlineData("ABC+123", "123")]
        [InlineData("_ABC#123_", "123")]
        [InlineData("02 1111 2222", "0211112222")]
        [InlineData("+61 2 1111 2222", "61211112222")]
        [InlineData("+61 02 1111 2222", "610211112222")]
        [InlineData("AGE:36", "36")]
        public void RemoveNonNumericalCharacters(string value, string expected)
        {
            string numericalValue = value.RemoveNonNumerical();

            numericalValue.ShouldBe(expected);
        }

        [Theory]
        [InlineData("A1B2C.3", "12.3", '.')]
        [InlineData("$1,135.0", "$1,135.0", '.', '$', ',')]
        [InlineData("Amount:$1,135.0", "$1,135.0", '.', '$', ',')]
        public void RemoveNonNumericalCharactersKeepingAllowed(string value, string expected, params char[] allowed)
        {
            string numericalValue = value.RemoveNonNumerical(allowed);

            numericalValue.ShouldBe(expected);
        }
    }
}