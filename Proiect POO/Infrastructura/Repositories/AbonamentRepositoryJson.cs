using Infrastructura.Persistence;
using Proiect_POO;

namespace Infrastructura.Repositories;

public class AbonamentRepositoryJson : JsonFileStocare<Abonament>
{
    public AbonamentRepositoryJson() : base("abonamente.json")
    {
    }
}