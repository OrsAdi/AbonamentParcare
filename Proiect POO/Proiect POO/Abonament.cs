namespace Proiect_POO;

public class Abonament
{
    public TipAbonament Tip { get; }
    public DateTime DataStart { get; }
    public bool Activ { get; private set; } = true;

    public Abonament(TipAbonament tip)
    {
        Tip = tip;
        DataStart = DateTime.Now;
    }

    public bool EsteExpirat()
        => DateTime.Now > DataStart.AddDays(Tip.ValabilitateZile);

    public void Anuleaza()
        => Activ = false;

    public override string ToString()
        => $"{Tip.Nume} | Activ: {Activ} | Expiră: {DataStart.AddDays(Tip.ValabilitateZile):d}";
}
