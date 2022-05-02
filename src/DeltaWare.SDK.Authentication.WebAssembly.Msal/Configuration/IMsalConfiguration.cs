using System.Collections.Generic;

namespace DeltaWare.SDK.Authentication.WebAssembly.Msal.Configuration
{
    public interface IMsalConfiguration
    {
        List<string> AdditionalScopes { get; }
        string Authority { get; }
        string ClientId { get; }
        List<string> DefaultScopes { get; }
        string LoginMode { get; }
        bool ValidateAuthority { get; }
    }
}