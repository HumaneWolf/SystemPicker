using System.Text.Json;

namespace SystemPicker.Matcher.Utils
{
    public class ConfiguredJson
    {
        private static JsonSerializerOptions _jsonOptions = new ()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };

        public static T Deserialize<T>(string raw)
        {
            return JsonSerializer.Deserialize<T>(raw, _jsonOptions);
        }
        
        public static string Serialize(object o)
        {
            return JsonSerializer.Serialize(o, _jsonOptions);
        }
    }
}