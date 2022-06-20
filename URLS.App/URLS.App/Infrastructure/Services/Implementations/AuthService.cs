using System.Net.Http.Json;
using URLS.App.Infrastructure.Helpers;
using URLS.App.Infrastructure.Models;
using URLS.App.Infrastructure.Services.Interfaces;
using Extensions.DeviceDetector.Models;

namespace URLS.App.Infrastructure.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Result<bool>> CloseSessionByIdAsync(Guid id)
        {
            return await _httpClient.DeleteFromJsonAsync<Result<bool>>($"api/v1/account/sessions/{id}");
        }

        public async Task<Result<bool>> CloseSessionsAsync(bool withCurrent = false)
        {
            return await _httpClient.DeleteFromJsonAsync<Result<bool>>($"api/v1/account/sessions?withCurrent={withCurrent}");
        }

        public async Task<Result<UserViewModel>> ConfigUserAsync(BlockUserModel model)
        {
            return await _httpClient.PostFromJsonAsync<Result<UserViewModel>>("api/v1/account/config", model);
        }

        public async Task<Result<object>> GetClaimsAsync()
        {
            return await _httpClient.GetFromJsonAsync<Result<object>>("api/v1/account/claims");
        }

        public async Task<Result<UserFullViewModel>> GetMeAsync()
        {
            return await _httpClient.GetFromJsonAsync<Result<UserFullViewModel>>("api/v1/account/me");
        }

        public async Task<Result<SessionViewModel>> GetSessionByIdAsync(Guid id)
        {
            return await _httpClient.GetFromJsonAsync<Result<SessionViewModel>>($"api/v1/account/sessions/{id}");
        }

        public async Task<Result<List<SessionViewModel>>> GetSessionsAsync(int q, int offset = 0, int limit = 10)
        {
            return await _httpClient.GetFromJsonAsync<Result<List<SessionViewModel>>>($"api/v1/account/sessions?q={q}&offset={offset}&limit={limit}");
        }

        public async Task<Result<List<SocialViewModel>>> GetUserSocialsAsync()
        {
            return await _httpClient.GetFromJsonAsync<Result<List<SocialViewModel>>>($"api/v1/account/socials");
        }

        public async Task<Result<AuthResponse>> LoginAsync(string username, string password)
        {
            var device = DeviceInfo.Current;

            var loginModel = new LoginCreateModel
            {
                App = AppCreds.GetApp(),
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
                        Type = device.DeviceType.ToString()
                    }
                },
                Login = username,
                Password = password
            };

            return await _httpClient.PostFromJsonAsync<Result<AuthResponse>>("api/v1/account/login", loginModel);
        }

        public async Task<Result<bool>> RegistrationAsync(RegisterUserViewModel registerViewModel)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<object>> RemoveSocialAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
