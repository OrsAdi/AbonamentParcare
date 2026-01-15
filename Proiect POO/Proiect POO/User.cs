using System.Text.Json.Serialization;

namespace Proiect_POO;

[JsonDerivedType(typeof(Admin), typeDiscriminator: "Admin")]
[JsonDerivedType(typeof(Client), typeDiscriminator: "Client")]
public abstract class User
{
    public string Username { get; }
    public string Password { get; }

    protected User(string username, string password)
    {
        Username = username;
        Password = password;
    }
}
