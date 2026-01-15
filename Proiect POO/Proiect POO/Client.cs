using System.Text.Json.Serialization; 

namespace Proiect_POO;

public class Client : User
{
    [JsonInclude]
    public List<Abonament> Abonamente { get; private set; } = new List<Abonament>();

    public Client(string username, string password)
        : base(username, password)
    {}
    
    public Client() {}
    
    public void AddAbonament(Abonament ab) {
        Abonamente.Add(ab); }
    
    public void AdaugaAbonament(Abonament abonament)
    {
        Abonamente.Add(abonament);
    }
 
}
