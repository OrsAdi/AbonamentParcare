using System.Text.Json;
using Infrastructura.Configuration;
using Proiect_POO;

namespace Infrastructura.Repositories;

public class TipAbonamentRepositoryJson
{
    private readonly string _filePath;

    public TipAbonamentRepositoryJson()
    {
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tipuri.json");
    }

    public List<TipAbonament> Incarca()
    {
        if (!File.Exists(_filePath)) return new List<TipAbonament>();

        try {
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<TipAbonament>>(json, JsonOptionsFactory.Create()) 
                   ?? new List<TipAbonament>();
        }
        catch { return new List<TipAbonament>(); }
    }

    public void Salveaza(List<TipAbonament> tipuri)
    {
        string json = JsonSerializer.Serialize(tipuri, JsonOptionsFactory.Create());
        File.WriteAllText(_filePath, json);
    }
}