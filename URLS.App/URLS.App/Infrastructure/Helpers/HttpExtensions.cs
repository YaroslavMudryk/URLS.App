using System.Net.Http.Json;
using URLS.App.Infrastructure.Models;
namespace URLS.App.Infrastructure.Helpers
{
    public static class HttpExtensions
    {
        public static async void CheckResponse(this HttpResponseMessage responseMessage)
        {
            if (!responseMessage.IsSuccessStatusCode)
            {
                var resposne = await responseMessage.Content.ReadFromJsonAsync<Result<Dictionary<string, string[]>>>();
                throw new Exception(resposne.Message);
            }
        }
    }
}