namespace Proiect_POO;

public class SubscriptionManager
{
    public List<User> Utilizatori { get; } = new List<User>();
    public List<Parcare> Parcari { get; } = new List<Parcare>();
    public List<TipAbonament> Tipuri { get; } = new List<TipAbonament>();

    public void AdaugaParcare(Parcare parcare)
    {
        Parcari.Add(parcare);
    }

    public void AdaugaTip(TipAbonament tip)
    {
        Tipuri.Add(tip);
    }

    public bool CumparaAbonament(Client client, TipAbonament tip)
    {
   
        bool areDejaAcestTip = client.Abonamente.Any(a => 
                a.Activ && 
                a.Tip.Nume == tip.Nume &&      
                !a.EsteExpirat()               
        );

        if (areDejaAcestTip)
        {
            Console.WriteLine($"[!] Ai deja un abonament activ de tipul '{tip.Nume}'!");
            return false;
        }

        var abonamentNou = new Abonament(tip);
        client.AdaugaAbonament(abonamentNou);
        return true;
    }
}