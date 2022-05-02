using System.Collections.Generic;

namespace DeltaWare.SDK.Authentication.WebAssembly.Msal.Configuration
{
    public interface IMsalConfigurationBuilder
    {
        List<string> AdditionalScopes { get; }
        string ClientId { get; set; }
        List<string> DefaultScopes { get; }
        string LoginMode { get; set; }
        bool ValidateAuthority { get; set; }
    }
}