using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace URLS.App.Infrastructure.Helpers
{
    public class HttpConvertor
    {
        public static HttpContent GetHttpContent(object data)
        {
            var con = JsonSerializer.Serialize(data);
            var content = new StringContent(con, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return content;
        }
    }
}
