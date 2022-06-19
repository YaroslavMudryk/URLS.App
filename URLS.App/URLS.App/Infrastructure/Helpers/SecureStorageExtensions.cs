using System.Text.Json;
using URLS.App.Infrastructure.Models;

namespace URLS.App.Infrastructure.Helpers
{
    public static class SecureStorageExtensions
    {
        private const string userKey = "Users";

        public static bool TryGetValue<T>(this ISecureStorage secureStorage, string key, out T data)
        {
            var result = secureStorage.GetAsync(key).Result;
            if (result != null)
            {
                data = JsonSerializer.Deserialize<T>(result);
                return true;
            }
            data = default;
            return false;
        }

        public static bool TryGetUsers(this ISecureStorage secureStorage, out List<SecureUserModel> users)
        {
            return secureStorage.TryGetValue(userKey, out users);
        }

        public static bool TrySetNewUser(this ISecureStorage secureStorage, SecureUserModel user, out string error)
        {
            if (secureStorage.TryGetUsers(out var users))
            {
                secureStorage.Remove(userKey);
                if (users.Any(s => s.Token == user.Token))
                {
                    error = "Can't set duplicate session";
                    return false;
                }
                user.Index = 1;
                users.Add(user);
                secureStorage.SetAsync(userKey, JsonSerializer.Serialize(users)).GetAwaiter().GetResult();
                error = null;
                return true;
            }
            user.Index = 1;
            secureStorage.SetAsync(userKey, JsonSerializer.Serialize(new List<SecureUserModel> { user })).GetAwaiter().GetResult();
            error = null;
            return true;
        }
    }

    public static class UserFullViewModelExtensions
    {
        public static SecureUserModel MapToSecure(this UserFullViewModel user, AuthResponse authResponse)
        {
            if(user == null)
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