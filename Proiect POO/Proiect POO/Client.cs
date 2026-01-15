using System.Text.Json.Serialization; 

namespace Proiect_POO;

public class Client : User
{
    public List<Abonament> Abonamente { get; private set; }

    public Client(string username, string password)
        : base(username, password)
    {
        Abonamente = new List<Abonament>();
    }
    [JsonConstructor]
    public Client(string username, string password, List<Abonament> abonamente)
        : base(username, password)
    {
        // Dacă lista e null (nu există în fișier), facem una goală
        Abonamente = abonamente ?? new List<Abonament>();
    }
}
