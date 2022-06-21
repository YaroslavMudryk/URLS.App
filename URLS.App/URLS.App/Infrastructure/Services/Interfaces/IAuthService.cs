using URLS.App.Infrastructure.Models;

namespace URLS.App.Infrastructure.Services.Interfaces
{
    public interface IAuthService
    {
        Task<Result<object>> GetClaimsAsync();
        Task<Result<UserFullViewModel>> GetMeAsync();
        Task<Result<List<SocialViewModel>>> GetUserSocialsAsync();
        Task<Result<bool>> RemoveSocialAsync(int id);
        Task<Result<AuthResponse>> LoginAsync(LoginCreateModel model);
        Task<Result<bool>> RegistrationAsync(RegisterUserViewModel registerViewModel);
        Task<Result<UserViewModel>> ConfigUserAsync(BlockUserModel model);
        Task<Result<List<SessionViewModel>>> GetSessionsAsync(int q, int offset = 0, int limit = 10);
        Task<Result<SessionViewModel>> GetSessionByIdAsync(Guid id);
        Task<Result<bool>> CloseSessionsAsync(bool withCurrent = false);
        Task<Result<bool>> CloseSessionByIdAsync(Guid id);
        HttpClient HttpClient { get; }
    }
}