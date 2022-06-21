using System.Text.Json;
using URLS.App.Infrastructure.Models;

namespace URLS.App.Infrastructure.Helpers
{
    public static class SecureStorageExtensions
    {
        public static bool TryGetValue<T>(this ISecureStorage secureStorage, string key, out T data)
        {
            var result = secureStorage.GetAsync(key).Result;
            if (result != null)
            {
                if (typeof(T) == typeof(string))
                    data = (T)(object)result;
                else
                    data = JsonSerializer.Deserialize<T>(result);
                return true;
            }
            data = default;
            return false;
        }

        public static async Task AuthorizeAsync(this ISecureStorage secureStorage, SecureUserModel user)
        {
            secureStorage.Remove(Constants.JwtKey);
            secureStorage.Remove(Constants.CurrentUser);

            await secureStorage.SetAsync(Constants.CurrentUser, user.ToJson());
            await secureStorage.SetAsync(Constants.JwtKey, user.Token);
        }

        public static void ClearAuthorize(this ISecureStorage secureStorage)
        {
            secureStorage.Remove(Constants.JwtKey);
            secureStorage.Remove(Constants.CurrentUser);
        }

        public static async Task<SecureUserModel> GetAuthorizeAsync(this ISecureStorage secureStorage)
        {
            var result = await secureStorage.GetAsync(Constants.CurrentUser);
            if (result == null)
                return null;
            return result.FromJson<SecureUserModel>();
        }

        public static async Task<string> GetTokenAsync(this ISecureStorage secureStorage)
        {
            return await secureStorage.GetAsync(Constants.JwtKey);
        }

        public static async Task<bool> IsAuthorizeAsync(this ISecureStorage secureStorage)
        {
            var token = await secureStorage.GetAsync(Constants.JwtKey);
            return token != null;
        }
    }
}