using System.Text.Json.Serialization;
namespace Proiect_POO;

public class Abonament
{
    public TipAbonament Tip { get; private set; }
    public DateTime DataStart { get; private set; }
    public bool Activ { get; private set; } = true;

    public Abonament(TipAbonament tip)
    {
        Tip = tip;
        DataStart = DateTime.Now;
        // Activ = true;
    }
    
    [JsonConstructor]
    public Abonament(TipAbonament tip, DateTime dataStart, bool activ)
    {
        Tip = tip;
        DataStart = dataStart;
        Activ = activ;
    }

    public bool EsteExpirat()
        => DateTime.Now > DataStart.AddDays(Tip.ValabilitateZile);

    public void Anuleaza()
        => Activ = false;

    // public override string ToString()
        // => $"{Tip.Nume} | Activ: {Activ} | Expiră: {DataStart.AddDays(Tip.ValabilitateZile):d}";
    
    public override string ToString()
    {
        var dataExpirare = DataStart.AddDays(Tip.ValabilitateZile);
        var status = Activ ? (EsteExpirat() ? "Expirat" : "Activ") : "Anulat";
        return $"[{status}] {Tip.Nume} (Zona {Tip.ZonaPermisa}) - Expira la: {dataExpirare:d}";
    }
    
}
