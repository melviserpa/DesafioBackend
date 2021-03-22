using System.Text.Json;
using System.Text.Json.Serialization;

namespace Investimentos.Custodia.CrossCutting.Helpers
{
    public static class JsonHelpers
    {
        public static JsonSerializerOptions GetJsonOptions()
        {
            return new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
            };
        }
    }
}
