using Proiect_POO; // Core entities
using Interfata.Output; // Output wrapper
using Interfata.Input;  // Input wrapper

namespace Interfata.Menus;

public class AdminMenu
{
    private readonly User _user;
    private readonly ConsoleOutput _out; // Inlocuieste Console
    private readonly ConsoleInput _in;   // Inlocuieste Console

    // Injectam dependintele prin constructor
    public AdminMenu(User user, ConsoleOutput output, ConsoleInput input)
    {
        _user = user;
        _out = output;
        _in = input;
    }

    public void Show()
    {
        while (true)
        {
            _out.WriteLine($"--- Panou Admin: {_user.Username} ---"); // Folosim _out, nu Console
            _out.WriteLine("1. Gestioneaza parcari");
            _out.WriteLine("2. Vezi abonamente");
            _out.WriteLine("0. Iesire");
            _out.Write("Optiune: ");

            var option = _in.ReadLine();

            switch (option)
            {
                case "1":
                    // Aici ai apela un serviciu, ex: _parkingService.Manage();
                    _out.WriteLine("Gestiune parcari...");
                    break;
                case "0":
                    return;
                default:
                    _out.WriteLine("Optiune invalida!");
                    break;
            }
        }
    }
}