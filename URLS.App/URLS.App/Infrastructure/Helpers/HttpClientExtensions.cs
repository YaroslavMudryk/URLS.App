using System.Net.Http.Json;
using System.Runtime.InteropServices;
using URLS.App.Infrastructure.Models;

namespace URLS.App.Infrastructure.Helpers
{
    public static class HttpClientExtensions
    {
        public static async Task<T> DeleteFromJsonAsync<T>(this HttpClient httpClient, string url)
        {
            var resposne = await httpClient.DeleteAsync(url);

            return await resposne.Content.ReadFromJsonAsync<T>();
        }

        public static async Task<T> PostFromJsonAsync<T>(this HttpClient httpClient, string uri, object body)
        {
            var resposne = await httpClient.PostAsync(uri, body.GetHttpContent());
            return await resposne.Content.ReadFromJsonAsync<T>();
        }
    }
}
