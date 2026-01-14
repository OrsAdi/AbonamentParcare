using Core.Entities;

namespace Interfata.Menus;

public class ClientMenu
{
    private readonly User _user;

    public ClientMenu(User user)
    {
        _user = user;
    }

    public void Show()
    {
        Console.WriteLine($"Client {_user.Name}");
        Console.WriteLine("1. Vezi abonamente");
        Console.WriteLine("2. Cumpara abonament");
    }
}