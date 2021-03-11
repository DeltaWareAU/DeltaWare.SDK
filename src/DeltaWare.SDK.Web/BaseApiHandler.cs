using DeltaWare.SDK.Web.Enums;
using DeltaWare.SDK.Web.Extensions;
using DeltaWare.SDK.Web.Interfaces;
using DeltaWare.SDK.Web.Types;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Web
{
    public abstract class BaseApiHandler<TEntity, TVersion>: IApiHandler<TEntity, TVersion> where TEntity : class where TVersion : IApiVersion
    {
        public abstract string BaseEndPoint { get; }

        public abstract Uri BaseUri { get; }

        public TimeSpan Timeout { get; set; } = new TimeSpan(0, 5, 0);

        public virtual async Task<IApiResponse<IEnumerable<TEntity>>> GetAsync(TVersion version)
        {
            return await PerformHttpActionAsync<string, IEnumerable<TEntity>>(ApiAction.Get, version, null);
        }

        public virtual async Task<IApiResponse<TEntity>> GetAsync(TVersion version, Guid identity)
        {
            return await PerformHttpActionAsync<Guid, TEntity>(ApiAction.Get, version, identity);
        }

        public virtual async Task<IApiResponse<TEntity>> CreateAsync(TVersion version, TEntity entity)
        {
            return await PerformHttpActionAsync<TEntity, TEntity>(ApiAction.Post, version, entity);
        }

        public virtual async Task<IApiResponse<TEntity>> DeleteAsync(TVersion version, Guid identity)
        {
            return await PerformHttpActionAsync<Guid, TEntity>(ApiAction.Delete, version, identity);
        }

        public virtual async Task<IApiResponse<TEntity>> UpdateAsync(TVersion version, TEntity entity)
        {
            return await PerformHttpActionAsync<TEntity, TEntity>(ApiAction.Put, version, entity);
        }

        protected virtual Task<HttpClient> GetHttpClientAsync()
        {
            return Task.Run(() =>
            {
                HttpClientHandler handler = new HttpClientHandler
                {
                    UseDefaultCredentials = true
                };

                HttpClient client = new HttpClient(handler)
                {
                    BaseAddress = BaseUri,
                    Timeout = Timeout
                };

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                return client;
            });
        }

        protected virtual async Task<IApiResponse<TResponse>> PerformHttpActionAsync<TContent, TResponse>(ApiAction action, TVersion version, string endPoint, TContent content)
        {
            using HttpClient client = await GetHttpClientAsync();

            if(client is null)
            {
                return ApiResponse<TResponse>.Failure("Could not Create an instance of the HttpClient");
            }

            HttpResponseMessage responseMessage;

            try
            {
                responseMessage = action switch
                {
                    ApiAction.Get => await client.GetAsync(GetRoute(version, endPoint, content?.ToString())),
                    ApiAction.Delete => await client.DeleteAsync(GetRoute(version, endPoint, content?.ToString())),
                    ApiAction.Post => await client.PostAsJsonAsync(GetRoute(version, endPoint), content),
                    ApiAction.Put => await client.PutAsJsonAsync(GetRoute(version, endPoint), content),
                    _ => throw new ArgumentOutOfRangeException(nameof(action), action, null)
                };
            }
            catch(Exception exception)
            {
                return ApiResponse<TResponse>.Failure("Http Action Failed, see exception", exception);
            }

            return await GetResponseAsync<TResponse>(responseMessage);
        }

        protected virtual async Task<IApiResponse<TResponse>> PerformHttpActionAsync<TContent, TResponse>(ApiAction action, TVersion version, TContent content)
        {
            using HttpClient client = await GetHttpClientAsync();

            if(client is null)
            {
                return ApiResponse<TResponse>.Failure("Could not Create an instance of the HttpClient");
            }

            HttpResponseMessage responseMessage;

            try
            {
                responseMessage = action switch
                {
                    ApiAction.Get => await client.GetAsync(GetRoute(version, content?.ToString())),
                    ApiAction.Delete => await client.DeleteAsync(GetRoute(version, content?.ToString())),
                    ApiAction.Post => await client.PostAsJsonAsync(GetRoute(version), content),
                    ApiAction.Put => await client.PutAsJsonAsync(GetRoute(version), content),
                    _ => throw new ArgumentOutOfRangeException(nameof(action), action, null)
                };
            }
            catch(Exception exception)
            {
                return ApiResponse<TResponse>.Failure("Http Action Failed, see exception", exception);
            }

            return await GetResponseAsync<TResponse>(responseMessage);
        }

        protected virtual async Task<IApiResponse<TResponse>> GetResponseAsync<TResponse>(HttpResponseMessage responseMessage)
        {
            if(responseMessage == null)
            {
                return ApiResponse<TResponse>.Failure("No Response was returned.");
            }

            if(!responseMessage.IsSuccessStatusCode)
            {
                return ApiResponse<TResponse>.Failure(responseMessage.ReasonPhrase);
            }

            try
            {
                TResponse response = await responseMessage.ReadAsJsonAsync<TResponse>();

                return ApiResponse<TResponse>.Success(response);
            }
            catch(Exception exception)
            {
                return ApiResponse<TResponse>.Failure("Could not deserialize returned value.", exception);

            }
        }

        protected virtual Uri GetRoute(IApiVersion version, string content = null)
        {
            if(string.IsNullOrEmpty(content))
            {
                return new Uri($"v{version.VersionString}/{BaseEndPoint}", UriKind.Relative);
            }

            return new Uri($"v{version.VersionString}/{BaseEndPoint}/{content}", UriKind.Relative);
        }


        protected virtual Uri GetRoute(IApiVersion version, string endPoint, string content)
        {
            return GetRoute(version, $"{endPoint}/{content}");
        }
    }
}
