using System.Text.Json;
using Infrastructura.Configuration;
using Proiect_POO; 

namespace Infrastructura.Persistence;

public class JsonFileStocare<T> where T : class
{
    private readonly string _filePath;
    
    private readonly JsonSerializerOptions _options;

    public JsonFileStocare(string fileName)
    {
        _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, fileName);
        _options = JsonOptionsFactory.Create(); 
    }

    public List<T> Incarca()
    {
        if (!File.Exists(_filePath))
        {
            return new List<T>();
        }

        try 
        {
            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json, _options) ?? new List<T>();
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Eroare la citire JSON {_filePath}: {ex.Message}");
            return new List<T>();
        }
    }

    public void Salveaza(List<T> data)
    {
        string json = JsonSerializer.Serialize(data, _options);
        File.WriteAllText(_filePath, json);
    }
}