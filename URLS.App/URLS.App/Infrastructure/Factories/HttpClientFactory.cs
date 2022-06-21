using System.Net.Http.Headers;

namespace URLS.App.Infrastructure.Factories
{
    public class HttpClientFactory
    {
        public static HttpClient CreateHttpClient()
        {
            var handler = new HttpClientHandler
            {
                AllowAutoRedirect = true,
                MaxAutomaticRedirections = 3,
                AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate,
                CheckCertificateRevocationList = false,
                ClientCertificateOptions = ClientCertificateOption.Manual,
                CookieContainer = new System.Net.CookieContainer(),
                Credentials = null,
                DefaultProxyCredentials = null,
                PreAuthenticate = false,
                SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls11,
                UseCookies = true,
                UseProxy = false,
                UseDefaultCredentials = false,
                Proxy = null,
#if ANDROID
                ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
                {
                    if (cert.Issuer.Equals("CN=localhost") || cert.Issuer.Equals("CN=Keyoti Conveyor Root Certificate Authority 2 - For development testing only!"))
                        return true;
                    return errors == System.Net.Security.SslPolicyErrors.None;
                }
#endif
            };

            var httpClient = new HttpClient(handler)
            {
                BaseAddress = new Uri("https://192.168.0.2:45455/"),
                Timeout = TimeSpan.FromMinutes(1)
            };

            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return httpClient;
        }
    }
}
