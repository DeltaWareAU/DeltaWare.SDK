using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AspNetCore.SwaggerUI
{
    public static class MsalSwaggerUIOptions
    {
        /// <summary>
        /// Adds button to authenticate against Microsoft Azure.
        /// </summary>
        public static void UseMsalAuthentication(this SwaggerUIOptions swaggerOptions, IConfiguration configuration, string key = "AzureAd")
        {
            MicrosoftIdentityOptions options = new MicrosoftIdentityOptions();

            configuration.Bind(key, options);

            swaggerOptions.OAuthClientId(options.ClientId);
            swaggerOptions.OAuthClientSecret(options.ClientSecret);
            swaggerOptions.OAuthUseBasicAuthenticationWithAccessCodeGrant();
        }
    }
}