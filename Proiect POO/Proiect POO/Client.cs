namespace Proiect_POO;

public class Client : User
{
    public List<Abonament> Abonamente { get; } = new();

    public Client(string username, string password)
        : base(username, password) { }
}
