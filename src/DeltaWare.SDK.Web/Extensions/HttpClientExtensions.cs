using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace DeltaWare.SDK.Web.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<TResponse> ReadAsJsonAsync<TResponse>(this HttpResponseMessage response)
        {
            string contentString = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TResponse>(contentString);
        }

        public static async Task<HttpResponseMessage> PostAsJsonAsync<TContent>(this HttpClient client, Uri requestUri, TContent content)
        {
            return await client.PostAsync(requestUri, new StringContent(JsonConvert.SerializeObject(content)));
        }

        public static async Task<HttpResponseMessage> PutAsJsonAsync<TContent>(this HttpClient client, Uri requestUri, TContent content)
        {
            return await client.PutAsync(requestUri, new StringContent(JsonConvert.SerializeObject(content)));
        }

        public static async Task<HttpResponseMessage> PatchAsJsonAsync<TContent>(this HttpClient client, Uri requestUri, TContent content)
        {
            return await client.PatchAsync(requestUri, new StringContent(JsonConvert.SerializeObject(content)));
        }
    }
}
