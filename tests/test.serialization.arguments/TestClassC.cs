
namespace DeltaWare.Tools.Serialization.Arguments.Tests
{
    using Attributes;

    class TestClassC
    {
        [Parameter("ValueSetA")]
        public static string ValueA { get; set; }
        [Flag("ValueSetB")]
        public static bool ValueB { get; set; }
    }
}
