using DeltaWare.SDK.Authentication.Web.Msal.Swagger.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace Swashbuckle.AspNetCore.SwaggerGen
{
    public static class MsalSwaggerGenOptions
    {
        /// <summary>
        /// Adds Microsoft Azure Authentication to Swagger.
        /// </summary>
        public static void UseMsalAuthentication(this SwaggerGenOptions swaggerOptions, Action<IAzureAdOptionsBuilder> options, IConfiguration configuration, string key = "AzureAd")
        {
            MicrosoftIdentityOptions msalOptions = new MicrosoftIdentityOptions();

            configuration.Bind(key, msalOptions);

            AzureAdOptions adOptions = new AzureAdOptions();

            options?.Invoke(adOptions);

            var permissions = adOptions.Permissions.ToDictionary(s => $"api://{msalOptions.ClientId}/{s.Key}", s => s.Value);

            swaggerOptions.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.OAuth2,
                Flows = new OpenApiOAuthFlows
                {
                    Implicit = new OpenApiOAuthFlow
                    {
                        AuthorizationUrl = new Uri($"{msalOptions.Instance}{msalOptions.TenantId}/oauth2/v2.0/authorize"),
                        TokenUrl = new Uri($"{msalOptions.Instance}{msalOptions.TenantId}/oauth2/v2.0/token"),
                        Scopes = permissions
                    }
                }
            });

            swaggerOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "oauth2"
                        },
                        Scheme = "oauth2",
                        Name = "oauth2",
                        In = ParameterLocation.Header
                    },
                    new List < string > ()
                }
            });
        }
    }
}