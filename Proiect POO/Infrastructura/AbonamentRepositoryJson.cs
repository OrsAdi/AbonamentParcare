using Proiect_POO;

namespace Infrastructura;

public class AbonamentRepositoryJson : JsonFileStocare<Abonament>
{
    public AbonamentRepositoryJson() : base("abonamente.json")
    {
    }
}