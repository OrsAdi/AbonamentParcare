using Interfata.Auth;
using Interfata.Menus;
using Core.Services;
using Infrastructure.Persistence;

var authService = new AuthService(new UserRepository());
var loginView = new LoginView(authService);

var user = loginView.Login();

if (user == null)
{
    Console.WriteLine("Autentificare esuata");
    return;
}

if (user.Role == "Admin")
{
    new AdminMenu(user).Show();
}
else
{
    new ClientMenu(user).Show();
}