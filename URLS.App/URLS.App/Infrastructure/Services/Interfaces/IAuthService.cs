using URLS.App.Infrastructure.Models;

namespace URLS.App.Infrastructure.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> LoginAsync(string username, string password);
        Task<bool> RegistrationAsync(RegisterUserViewModel registerViewModel);
        Task<UserFullViewModel> GetMeAsync(string token);
    }
}
