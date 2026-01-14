using Core.Entities;
using Core.Services;

namespace Interfata.Auth;

public class LoginView
{
    private readonly IAuthService _authService;

    public LoginView(IAuthService authService)
    {
        _authService = authService;
    }

    public User? Login()
    {
        Console.Write("Email: ");
        var email = Console.ReadLine();

        Console.Write("Parola: ");
        var password = Console.ReadLine();

        return _authService.Login(email!, password!);
    }
}

