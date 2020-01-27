using DeltaWare.SDK.Web.Enums;
using DeltaWare.SDK.Web.Extensions;
using DeltaWare.SDK.Web.Interfaces;
using DeltaWare.SDK.Web.Types;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Web
{
    public abstract class BaseApiHandler<TVersion> where TVersion : IApiVersion
    {
        protected string BaseEndPoint { get; }

        protected Uri BaseUri { get; }

        public TimeSpan Timeout { get; set; } = new TimeSpan(0, 5, 0);

        protected BaseApiHandler(Uri baseUri, string baseEndPoint)
        {
            BaseUri = baseUri;
            BaseEndPoint = baseEndPoint;
        }

        protected virtual async Task<IApiResponse<TResponse>> PerformHttpActionAsync<TContent, TResponse>(ApiAction action, IApiVersion version, string endPoint, TContent content)
        {
            CheckApiVersionType(version);

            using HttpClient client = GetHttpClient();

            var responseMessage = action switch
            {
                ApiAction.Get => await client.GetAsync(GetRoute(version, endPoint, content.ToString())),
                ApiAction.Delete => await client.DeleteAsync(GetRoute(version, endPoint, content.ToString())),
                ApiAction.Post => await client.PostAsJsonAsync(GetRoute(version, endPoint), content),
                ApiAction.Put => await client.PutAsJsonAsync(GetRoute(version, endPoint), content),
                _ => throw new ArgumentOutOfRangeException(nameof(action), action, null)
            };

            return await GetResponseAsync<TResponse>(responseMessage);
        }

        protected virtual async Task<IApiResponse<TResponse>> PerformHttpActionAsync<TContent, TResponse>(ApiAction action, IApiVersion version, TContent content)
        {
            CheckApiVersionType(version);

            using HttpClient client = GetHttpClient();

            var responseMessage = action switch
            {
                ApiAction.Get => await client.GetAsync(GetRoute(version, content.ToString())),
                ApiAction.Delete => await client.DeleteAsync(GetRoute(version, content.ToString())),
                ApiAction.Post => await client.PostAsJsonAsync(GetRoute(version), content),
                ApiAction.Put => await client.PutAsJsonAsync(GetRoute(version), content),
                _ => throw new ArgumentOutOfRangeException(nameof(action), action, null)
            };

            return await GetResponseAsync<TResponse>(responseMessage);
        }

        protected virtual async Task<IApiResponse<TResponse>> GetResponseAsync<TResponse>(HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                return ApiResponse<TResponse>.Failure();
            }

            TResponse response = await responseMessage.ReadAsJsonAsync<TResponse>();

            return ApiResponse<TResponse>.Success(response);
        }

        protected virtual Uri GetRoute(IApiVersion version, string content = null)
        {
            if (string.IsNullOrEmpty(content))
            {
                return new Uri($"api/{version.VersionString}/{BaseUri}");
            }

            return new Uri($"api/{version.VersionString}/{BaseUri}/{content}");
        }


        protected virtual Uri GetRoute(IApiVersion version, string endPoint, string content)
        {
            return GetRoute(version, $"{endPoint}/{content}");
        }

        protected virtual HttpClient GetHttpClient()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = BaseUri,
                Timeout = Timeout
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        protected virtual TVersion CheckApiVersionType(IApiVersion apiVersion)
        {
            if (apiVersion is TVersion version)
            {
                return version;
            }

            throw new ArgumentException();
        }
    }
}
