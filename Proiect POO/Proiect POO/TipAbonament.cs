using System.Text.Json.Serialization;
namespace Proiect_POO;

public class TipAbonament
{
    public string Nume { get; }
    public decimal Pret { get; }
    public int ValabilitateZile { get; }
    public string ZonaPermisa { get; }
    [JsonConstructor]
    public TipAbonament(string nume, decimal pret, int zile, string zona)
    {
        Nume = nume;
        Pret = pret;
        ValabilitateZile = zile;
        ZonaPermisa = zona;
    }

    public override string ToString()
        => $"{Nume} | {Pret} RON | {ValabilitateZile} zile | Zona {ZonaPermisa}";
}
