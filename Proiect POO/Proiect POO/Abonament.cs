using System.Text.Json.Serialization;
namespace Proiect_POO;

public class Abonament{
    [JsonInclude]
    public TipAbonament Tip { get; private set; }
    [JsonInclude]
    public DateTime DataStart { get; private set; }
    [JsonInclude]
    public bool Activ { get; private set; } = true;

    public Abonament(TipAbonament tip)
    {
        Tip = tip;
        DataStart = DateTime.Now;
    }
    
   

    public Abonament() { }
    public bool EsteExpirat()
        => DateTime.Now > DataStart.AddDays(Tip.ValabilitateZile);

    public void Anuleaza()
        => Activ = false;
    
    
    public override string ToString()
    {
        var dataExpirare = DataStart.AddDays(Tip?.ValabilitateZile ?? 30);
        var status = Activ ? (EsteExpirat() ? "Expirat" : "Activ") : "Anulat";
        return $"[{status}] {Tip.Nume} (Zona {Tip.ZonaPermisa}) - Expira la: {dataExpirare:d}";
    }
    
}
