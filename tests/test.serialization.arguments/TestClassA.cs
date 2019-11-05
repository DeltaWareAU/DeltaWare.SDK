
using DeltaWare.SDK.Serialization.Arguments.Attributes;

namespace DeltaWare.SDK.Tests.Serialization.Arguments
{
    public class TestClassA
    {
        [Parameter]
        public static string ValueA { get; set; }

        [Parameter]
        public static int ValueB { get; set; }

        [Flag]
        public static bool ValueC { get; set; }
    }
}
