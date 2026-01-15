using System.Text.Json.Serialization;

namespace Proiect_POO;

[JsonDerivedType(typeof(Admin), typeDiscriminator: "Admin")]
[JsonDerivedType(typeof(Client), typeDiscriminator: "Client")]
public abstract class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    [JsonConstructor]
    protected User(string username, string password)
    {
        Username = username;
        Password = password;
    }

    public User() { }
}
