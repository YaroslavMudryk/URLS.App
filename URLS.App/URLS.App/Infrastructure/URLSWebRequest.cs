using System.Net;
using URLS.App.Infrastructure.Exceptions;
using URLS.App.Infrastructure.Helpers;

namespace URLS.App.Infrastructure
{
    public class URLSWebRequest
    {
        private readonly HttpClient _httpClient;
        public URLSWebRequest(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> PostAsync(string url, object data)
        {
            return await PostAuthorizeAsync(url, data, null);
        }

        public async Task<HttpResponseMessage> PostAuthorizeAsync(string url, object data, string accessToken)
        {
            if (!string.IsNullOrWhiteSpace(accessToken))
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var httpContent = data.GetHttpContent();
            var response = await _httpClient.PostAsync(url, httpContent);
            CheckAuthResponse(response);
            return response;
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            return await GetAuthorizeAsync(url, null);
        }

        public async Task<HttpResponseMessage> GetAuthorizeAsync(string url, string accessToken)
        {
            if (!string.IsNullOrWhiteSpace(accessToken))
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync(url);
            CheckAuthResponse(response);
            return response;
        }



        public async Task<HttpResponseMessage> PutAsync(string url, object data)
        {
            return await PutAuthorizeAsync(url, data, null);
        }

        public async Task<HttpResponseMessage> PutAuthorizeAsync(string url, object data, string accessToken)
        {
            if (!string.IsNullOrWhiteSpace(accessToken))
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var httpContent = data.GetHttpContent();
            var response = await _httpClient.PutAsync(url, httpContent);
            CheckAuthResponse(response);
            return response;
        }



        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return await DeleteAuthorizeAsync(url, null);
        }

        public async Task<HttpResponseMessage> DeleteAuthorizeAsync(string url, string accessToken)
        {
            if (!string.IsNullOrWhiteSpace(accessToken))
                _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.DeleteAsync(url);
            CheckAuthResponse(response);
            return response;
        }

        public void CheckAuthResponse(HttpResponseMessage httpResponse)
        {
            if (httpResponse.StatusCode == HttpStatusCode.Unauthorized)
                throw new UnauthorizedException("User is unauthorized");
        }
    }
}
