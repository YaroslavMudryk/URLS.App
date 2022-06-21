using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace URLS.App.Infrastructure.Helpers
{
    public static class HttpClientExtensions
    {
        public static async Task<T> DeleteFromJsonAsync<T>(this HttpClient httpClient, string url)
        {
            httpClient.SetBearerToken();
            var resposne = await httpClient.DeleteAsync(url);
            return await resposne.Content.ReadFromJsonAsync<T>();
        }

        public static async Task<T> PostFromJsonAsync<T>(this HttpClient httpClient, string uri, object body)
        {
            httpClient.SetBearerToken();
            var resposne = await httpClient.PostAsync(uri, body.GetHttpContent());
            return await resposne.Content.ReadFromJsonAsync<T>();
        }

        public static async Task<T> GetFromJsonAsync<T>(this HttpClient httpClient, string url)
        {
            httpClient.SetBearerToken();
            var response = await httpClient.GetAsync(url);
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public static async Task<T> PatchFromJsonAsync<T>(this HttpClient httpClient, string url, object body)
        {
            httpClient.SetBearerToken();
            var response = await httpClient.PatchAsync(url, body.GetHttpContent());
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public static void SetBearerToken(this HttpClient httpClient, string newToken = null)
        {
            if (string.IsNullOrEmpty(newToken))
            {
                if (!httpClient.DefaultRequestHeaders.Contains("Authorization"))
                {
                    if (SecureStorage.Default.TryGetValue<string>(Constants.JwtKey, out var token))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                    }
                }
            }
            else
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", newToken);
        }
    }
}
