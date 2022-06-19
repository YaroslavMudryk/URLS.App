using System.Text.Json;
namespace URLS.App.Infrastructure.Helpers
{
    public static class JsonConverterExtensions
    {
        public static T GetObjectFromJson<T>(this string json)
        {
            if (json == null)
                return default;
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}