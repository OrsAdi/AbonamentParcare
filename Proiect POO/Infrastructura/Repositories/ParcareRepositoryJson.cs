using System.Text.Json;
using Infrastructura.Configuration;
using Proiect_POO;

namespace Infrastructura.Repositories;

public class ParcareRepositoryJson
{
    private readonly string _filePath;

    public ParcareRepositoryJson()
    {
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "parcari.json");
    }

    public List<Parcare> Incarca()
    {
        if (!File.Exists(_filePath)) return new List<Parcare>();
        
        try {
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Parcare>>(json, JsonOptionsFactory.Create()) 
                   ?? new List<Parcare>();
        }
        catch { return new List<Parcare>(); }
    }

    public void Salveaza(List<Parcare> parcari)
    {
        string json = JsonSerializer.Serialize(parcari, JsonOptionsFactory.Create());
        File.WriteAllText(_filePath, json);
    }
}