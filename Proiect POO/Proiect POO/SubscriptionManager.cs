namespace Proiect_POO;

public class SubscriptionManager
{
    public List<Parcare> Parcari { get; } = new();
    public List<TipAbonament> Tipuri { get; } = new();
    public List<User> Utilizatori { get; } = new();

    public void AdaugaParcare(Parcare p) => Parcari.Add(p);
    public void AdaugaTip(TipAbonament t) => Tipuri.Add(t);

    public void CumparaAbonament(Client client, TipAbonament tip)
    {
        client.Abonamente.Add(new Abonament(tip));
    }
}
