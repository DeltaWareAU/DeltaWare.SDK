namespace DeltaWare.SDK.Authentication.Web.Msal.Swagger.Configuration
{
    public interface IAzureAdOptionsBuilder
    {
        void RegisterPermission(string name, string description = null);
    }
}