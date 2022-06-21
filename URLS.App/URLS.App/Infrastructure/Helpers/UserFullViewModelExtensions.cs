using URLS.App.Infrastructure.Models;

namespace URLS.App.Infrastructure.Helpers
{
    public static class UserFullViewModelExtensions
    {
        public static SecureUserModel MapToSecure(this UserFullViewModel user, AuthResponse authResponse)
        {
            if (user == null)
                return null;
            return new SecureUserModel
            {
                Id = user.Id,
                Token = authResponse.Token,
                ExpiredAt = authResponse.ExpiredAt,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                FullName = user.FullName,
                Image = user.Image,
                UserName = user.UserName
            };
        }
    }
}