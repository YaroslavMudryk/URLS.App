using System.Text.Json;
using System.Text.Json.Serialization;

namespace URLS.App.Infrastructure.Helpers
{
    public static class JsonExtensions
    {
        private readonly static JsonSerializerOptions defaultOptions = new JsonSerializerOptions
        {
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        };

        public static string ToJson<T>(this T data, JsonSerializerOptions options = null)
        {
            options = options ?? defaultOptions;

            return JsonSerializer.Serialize(data, options);
        }

        public static T FromJson<T>(this string json, JsonSerializerOptions options = null)
        {
            options = options ?? defaultOptions;

            return JsonSerializer.Deserialize<T>(json, options);
        }
    }
}