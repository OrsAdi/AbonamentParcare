using System.Text.Json.Serialization;

namespace Proiect_POO;

public class TipAbonament
{
    [JsonInclude]
    public string Nume { get; private set; }
    
    [JsonInclude]
    public decimal Pret { get; private set; }
    
    [JsonInclude]
    public int ValabilitateZile { get; private set; }
    
    [JsonInclude]
    public string ZonaPermisa { get; private set; }

    public TipAbonament(string nume, decimal pret, int valabilitateZile, string zonaPermisa)
    {
        Nume = nume;
        Pret = pret;
        ValabilitateZile = valabilitateZile;
        ZonaPermisa = zonaPermisa;
    }

    public TipAbonament() { }

    public override string ToString() => $"{Nume} | {Pret} RON | {ValabilitateZile} zile | Zona {ZonaPermisa}";
}