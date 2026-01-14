using System.Text.Json;
using Proiect_POO; // Referinta catre Core pentru entitati daca e nevoie

namespace Infrastructura.Persistence;

// Aceasta clasa este GENERICĂ. T poate fi User, Abonament, Parcare, etc.
// where T : class constrange T sa fie un tip de referinta.
public class JsonFileStocare<T> where T : class
{
    private readonly string _filePath;
    
    // Folosim factory-ul tau pentru optiuni consistente
    private readonly JsonSerializerOptions _options;

    public JsonFileStocare(string fileName)
    {
        // Setam calea relativa catre fisier
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
            // Aici ar trebui sa folosim Logger-ul, dar momentan scriem la consola de debug
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