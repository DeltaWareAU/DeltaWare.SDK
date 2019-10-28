
namespace DeltaWare.Tools.Serialization.Arguments.Tests
{
    using DeltaWare.Tools.Serialization.Arguments.Attributes;

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
