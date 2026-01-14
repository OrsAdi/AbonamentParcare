namespace Proiect_POO;

public static class LoginService
{
    public static User Login(List<User> utilizatori)
    {
        Console.Write("Username: ");
        string username = Console.ReadLine();

        Console.Write("Password: ");
        string password = Console.ReadLine();

        foreach (var u in utilizatori)
        {
            if (u.Username == username && u.Password == password)
            {
                Console.WriteLine("Login reusit!");
                return u;
            }
        }

        Console.WriteLine("Username sau parola incorecte!");
        return null;
    }
}
