using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructura.Configuration;

public static class JsonOptionsFactory
{
    public static JsonSerializerOptions Create()
    {
        return new JsonSerializerOptions
        {
            WriteIndented = true, // Sa arate frumos in fisier
            PropertyNameCaseInsensitive = true,
            // Foarte important pentru a salva derivate (Admin/Client) in lista de baza (User)
            UnknownTypeHandling = JsonUnknownTypeHandling.JsonElement 
        };
    }
}