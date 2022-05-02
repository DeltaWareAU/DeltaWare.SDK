using DeltaWare.SDK.Authentication.WebAssembly.Msal.Configuration;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using System;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    public static class MsalServiceCollection
    {
        /// <summary>
        /// Enables the application to authenticate against Microsoft Azure.
        /// </summary>
        public static void UseMsalAuthentication(this IServiceCollection services, Action<IMsalConfigurationBuilder> builder)
        {
            MsalConfiguration configuration = new MsalConfiguration();

            builder.Invoke(configuration);

            UseMsalAuthentication(services, configuration);
        }

        /// <summary>
        /// Enables the application to authenticate against Microsoft Azure.
        /// </summary>
        public static void UseMsalAuthentication(this IServiceCollection services, WebAssemblyHostConfiguration hostConfiguration, string key = "AzureAd")
        {
            MsalConfiguration configuration = new MsalConfiguration();

            hostConfiguration.Bind(key, configuration);

            UseMsalAuthentication(services, configuration);
        }

        private static void UseMsalAuthentication(IServiceCollection services, IMsalConfiguration configuration)
        {
            services.AddSingleton(configuration);

            services.AddMsalAuthentication(options =>
            {
                options.ProviderOptions.Authentication.Authority = configuration.Authority;
                options.ProviderOptions.Authentication.ClientId = configuration.ClientId;
                options.ProviderOptions.Authentication.ValidateAuthority = configuration.ValidateAuthority;
                options.ProviderOptions.LoginMode = configuration.LoginMode;

                foreach (string scope in configuration.DefaultScopes)
                {
                    options.ProviderOptions.DefaultAccessTokenScopes.Add(scope);
                }

                foreach (string scope in configuration.AdditionalScopes)
                {
                    options.ProviderOptions.AdditionalScopesToConsent.Add(scope);
                }
            });
        }
    }
}