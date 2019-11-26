
using DeltaWare.SDK.Serialization.Arguments.Attributes;

namespace DeltaWare.SDK.Tests.Serialization.Arguments.Models
{
    public class ModelA
    {
        [Parameter]
        public static string ValueA { get; set; }

        [Parameter]
        public static int ValueB { get; set; }

        [Flag]
        public static bool ValueC { get; set; }
    }
}
