using Extensions.DeviceDetector.Models;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using URLS.App.Infrastructure.Helpers;
using URLS.App.Infrastructure.Models;
using URLS.App.Infrastructure.Services.Interfaces;

namespace URLS.App.Infrastructure.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(string baseUrl)
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
                ServerCertificateCustomValidationCallback = null,
                SslProtocols = System.Security.Authentication.SslProtocols.Tls12 | System.Security.Authentication.SslProtocols.Tls11,
                UseCookies = true,
                UseProxy = false,
                UseDefaultCredentials = false,
                Proxy = null
            };
#if ANDROID
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost") || cert.Issuer.Equals("CN=Keyoti Conveyor Root Certificate Authority 2 - For development testing only!"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
#endif

            _httpClient = new HttpClient(handler);
            _httpClient.Timeout = TimeSpan.FromMinutes(5);
            _httpClient.BaseAddress = new Uri(baseUrl);
        }

        public async Task<UserFullViewModel> GetMeAsync(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var resposne = await _httpClient.GetAsync("api/v1/users/me");

            resposne.CheckResponse();

            return (await resposne.Content.ReadFromJsonAsync<Result<UserFullViewModel>>()).Data;
        }

        public async Task<AuthResponse> LoginAsync(string username, string password)
        {
            var device = DeviceInfo.Current;

            var body = new LoginCreateModel
            {
                Login = username,
                Password = password,
                App = new AppLoginCreateModel
                {
                    Id = "F7Gv-2mu8-rH7Z-U4pt",
                    Secret = "bUqNPgQhJoj8oHyIsz2cwICuxTCGo0oBLDaBODiQ0pOLnLn6H99IEJipavIwHhbEzf5Y4s",
                    Version = "0.1 Beta"
                },
                Client = new ClientInfo
                {
                    Browser = null,
                    OS = new OS
                    {
                        Name = device.Platform.ToString(),
                        Version = device.VersionString
                    },
                    Device = new Extensions.DeviceDetector.Models.Device
                    {
                        Brand = device.Manufacturer,
                        Model = device.Model,
                        Type = device.Idiom.ToString()
                    }
                }
            };

            var response = await _httpClient.PostAsync("api/v1/account/login", body.GetHttpContent());

            response.CheckResponse();

            return (await response.Content.ReadFromJsonAsync<Result<AuthResponse>>()).Data;
        }

        public async Task<bool> RegistrationAsync(RegisterUserViewModel registerViewModel)
        {
            var resposne = await _httpClient.PostAsync("api/v1/account/registration", registerViewModel.GetHttpContent());

            resposne.CheckResponse();

            return resposne.IsSuccessStatusCode;
        }
    }
}
