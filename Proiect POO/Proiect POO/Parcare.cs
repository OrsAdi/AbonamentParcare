using System.Text.Json.Serialization;
namespace Proiect_POO;

public class Parcare
{
    [JsonInclude]
    public string Nume { get; private set; }
    [JsonInclude]
    public string Adresa { get; private set; }
    [JsonInclude]
    public int LocuriTotale { get; private set; }

    public Parcare(string nume, string adresa, int locuriTotale)
    {
        Nume = nume;
        Adresa = adresa;
        LocuriTotale = locuriTotale;
    }

    public Parcare() { }
    public override string ToString()
        => $"{Nume} | Zona: {Adresa} | Capacitate: {LocuriTotale}";
}
