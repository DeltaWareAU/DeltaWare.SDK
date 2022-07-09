using System.Collections.Generic;

namespace DeltaWare.SDK.Authentication.Web.Msal.Swagger.Configuration
{
    internal class AzureAdOptions : IAzureAdOptionsBuilder
    {
        public Dictionary<string, string> Permissions { get; } = new Dictionary<string, string>();

        public void RegisterPermission(string name, string description = null)
        {
            Permissions.Add(name, description);
        }
    }
}