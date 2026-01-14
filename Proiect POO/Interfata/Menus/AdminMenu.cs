using Core.Entities;

namespace Interfata.Menus;

public class AdminMenu
{
    private readonly User _user;

    public AdminMenu(User user)
    {
        _user = user;
    }

    public void Show()
    {
        Console.WriteLine($"Admin {_user.Name}");
        Console.WriteLine("1. Gestioneaza parcari");
        Console.WriteLine("2. Vezi abonamente");
    }
}