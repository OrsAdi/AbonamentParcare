namespace Proiect_POO;

public class Parcare
{
    public string Nume { get; }
    public string Zona { get; }
    public int Capacitate { get; }

    public Parcare(string nume, string zona, int capacitate)
    {
        Nume = nume;
        Zona = zona;
        Capacitate = capacitate;
    }

    public override string ToString()
        => $"{Nume} | Zona: {Zona} | Capacitate: {Capacitate}";
}
