using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructura.Configuration;

public static class JsonOptionsFactory
{
    public static JsonSerializerOptions Create()
    {
        return new JsonSerializerOptions
        {
            WriteIndented = true, 
            PropertyNameCaseInsensitive = true,
            UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement 
        };
    }
}