using System.Collections.Generic;

namespace DeltaWare.SDK.Authentication.WebAssembly.Msal.Configuration
{
    public class MsalConfiguration : IMsalConfiguration, IMsalConfigurationBuilder
    {
        public List<string> AdditionalScopes { get; } = new List<string>();
        public string Authority => "https://login.microsoftonline.com/ef333ca6-2594-4d26-908a-0792888640b6";

        public string ClientId { get; set; }
        public List<string> DefaultScopes { get; } = new List<string>();

        public string LoginMode { get; set; } = "redirect";

        public bool ValidateAuthority { get; set; }
    }
}