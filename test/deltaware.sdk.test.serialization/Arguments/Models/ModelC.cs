
using DeltaWare.SDK.Serialization.Arguments.Attributes;

namespace DeltaWare.SDK.Tests.Serialization.Arguments.Models
{
    class ModelC
    {
        [Parameter("ValueSetA")]
        public static string ValueA { get; set; }
        [Flag("ValueSetB")]
        public static bool ValueB { get; set; }
    }
}
