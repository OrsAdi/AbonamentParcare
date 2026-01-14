using Interfata.Auth;
using Interfata.Menus;
using Interfata.Input;
using Interfata.Output;
using Infrastructura.Repositories;
using Proiect_POO; // Core

// 1. Setup Infrastructura (Repository-uri)
var userRepo = new UserRepositoryJson();
// var abonamentRepo = new AbonamentRepositoryJson();

// 2. Setup UI Helpers
var consoleIn = new ConsoleInput();
var consoleOut = new ConsoleOutput();

// 3. Setup Servicii (Presupunand ca colegii au facut AuthService care cere un repo)
// Daca AuthService nu exista inca sau nu accepta repo, il simulam sau adaptam:
// var authService = new AuthService(userRepo); 

// Nota: LoginView trebuie adaptat sa primeasca si el Input/Output pentru a fi consistent
// var loginView = new LoginView(authService, consoleOut, consoleIn);

// MOMENTAN, pentru a testa codul tau de infrastructura direct:
consoleOut.WriteLine("Incarcare utilizatori din JSON...");
var users = userRepo.Incarca();

if (users.Count == 0)
{
    consoleOut.WriteLine("Nu s-au gasit useri. Se creeaza default admin/client...");
    users.Add(new Admin("admin", "admin")); // Atentie: Asigura-te ca Admin are constructorul corect
    users.Add(new Client("client", "client"));
    userRepo.Salveaza(users); // Testam salvarea
}

// Logica de login simplificata pentru demo (sau folosesti LoginService-ul colegilor)
var currentUser = LoginService.Login(users); // Folosind serviciul static din Core

if (currentUser != null)
{
    if (currentUser is Admin)
    {
        new AdminMenu(currentUser, consoleOut, consoleIn).Show();
    }
    else if (currentUser is Client)
    {
        new ClientMenu(currentUser).Show(); // Va trebui adaptat si ClientMenu similar cu AdminMenu
    }
}
else
{
    consoleOut.WriteLine("Login esuat sau anulat.");
}