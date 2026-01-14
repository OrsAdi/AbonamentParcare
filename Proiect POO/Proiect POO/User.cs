namespace Proiect_POO;

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
