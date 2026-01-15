using Proiect_POO;
using Interfata.Menus;
using Infrastructura.Repositories;
using Interfata.Input;
using Interfata.Output; // Dacă folosești repository-uri, altfel șterge linia

class Program
{
    static void Main(string[] args)
    {

        // 1. CONFIGURARE (Se execută o singură dată la start)
        var consoleOut = new ConsoleOutput();
        var consoleIn = new ConsoleInput();

        // Setup Date
        var manager = new SubscriptionManager();

        // Încărcare din repository (sau date de test)
        var userRepo = new UserRepositoryJson();
        var users = userRepo.Incarca();

        if (users.Count == 0)
        {
            users.Add(new Admin("admin", "admin"));
            userRepo.Salveaza(users);
        }

        // Populăm managerul
        foreach (var u in users) manager.Utilizatori.Add(u);

        // Date parcare (hardcodate pt exemplu)
        manager.AdaugaParcare(new Parcare("Central Parking", "A", 100));
        manager.AdaugaParcare(new Parcare("Complex", "B", 100));
        manager.AdaugaParcare(new Parcare("Isho", "C", 100));

        manager.AdaugaTip(new TipAbonament("Standard", 150, 30, "A"));
        manager.AdaugaTip(new TipAbonament("Premium", 300, 30, "A"));
        manager.AdaugaTip(new TipAbonament("Standard", 100, 30, "B"));
        manager.AdaugaTip(new TipAbonament("Premium", 300, 30, "B"));
        manager.AdaugaTip(new TipAbonament("Standard", 200, 30, "C"));
        manager.AdaugaTip(new TipAbonament("Premium", 300, 30, "C"));


        // 2. BUCLA INFINITĂ (Aici este secretul interactivității)
        while (true)
        {
            Console.Clear(); // Curăță ecranul ca să arate proaspăt
            consoleOut.WriteLine("=== SISTEM PARCARE ===");
            consoleOut.WriteLine("1. Autentificare");
            consoleOut.WriteLine("2. Inregistrare");
            consoleOut.WriteLine("3. Iesire");
            consoleOut.Write("Alege optiunea: ");

            var comanda = consoleIn.ReadLine();

            switch (comanda)
            {
                case "1":
                    ProcesLogin(manager, userRepo);
                    break;
                case "2":
                    ProcesInregistrare(manager, userRepo);
                    break;
                case "3":
                    consoleOut.WriteLine("La revedere!");
                    return;
                default:
                    consoleOut.WriteLine("Optiune invalida");
                    Thread.Sleep(1000);
                    break;
            }
        }
    }

    static void ProcesLogin(SubscriptionManager manager, UserRepositoryJson userRepo)
    {
        var user = LoginService.Login(manager.Utilizatori);
        if (user != null)
        {
            Console.WriteLine($"Bine ai venit, {user.Username}!");
            Thread.Sleep(1000);
            
            if(user is Admin adminUser)
            {
                new AdminMenu(adminUser,manager,userRepo).Show();
            }
            else if (user is Client clientUser)
            {
                new ClientMenu(clientUser,manager).Show();
            }
        }
        else
        {
            Console.WriteLine("Apasa Enter pentru a contiuna..");
            Console.ReadLine();
        }

    }

    static void ProcesInregistrare(SubscriptionManager manager, UserRepositoryJson userRepo)
    {
        Console.Clear();
        Console.WriteLine("---CREARE CONT CLIENT---");
        
        Console.Write("Alege un Username:");
        string username = Console.ReadLine();

        if (manager.Utilizatori.Any(u => u.Username == username))
        {
            Console.WriteLine("Eroare: Acest username este deja luat!Incearcati altul");
            Console.ReadLine();
            return;
        }
        
        Console.WriteLine("Alege o Parola: ");
        string password = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Eroare: Datele nu pot fi goale");
            Console.ReadLine();
            return;
        }
        
        var clientNou = new Client(username, password);
        
        manager.Utilizatori.Add(clientNou);

        try
        {
            userRepo.Salveaza(manager.Utilizatori);
            Console.WriteLine("Cont creat cu succes! Te poti autentifica acum");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Eraore la salvare: {e.Message}");
            manager.Utilizatori.Remove(clientNou);
        }
        Console.WriteLine("Apasa Enter pentru a reveni la meniu..");
        Console.ReadLine();
    }

    // if (comanda == "2")
        //     {
        //         consoleOut.WriteLine("La revedere!");
        //         break; // Sparge bucla while și închide programul
        //     }
        //     else if (comanda == "1")
        //     {
        //         // Încercăm logarea
        //         var user = LoginService.Login(manager.Utilizatori);
        //
        //         if (user != null)
        //         {
        //             consoleOut.WriteLine($"Bine ai venit, {user.Username}!");
        //             Thread.Sleep(1000); // Mică pauză estetică
        //
        //             // Deschidem meniul specific
        //             // Când metoda Show() se termină (la logout), revenim aici
        //             if (user is Admin adminUser)
        //             {
        //                 new AdminMenu(adminUser, manager).Show();
        //             }
        //             else if (user is Client clientUser)
        //             {
        //                 new ClientMenu(clientUser, manager).Show();
        //             }
        //         }
        //         else
        //         {
        //             consoleOut.WriteLine("Autentificare esuata. Apasa Enter pentru a continua...");
        //             consoleIn.ReadLine();
        //         }
        //     }
        // }
    
    
    static void InitializareDateDemo(SubscriptionManager manager)
    {
        manager.Utilizatori.Add(new Admin("admin", "admin"));
        manager.Utilizatori.Add(new Client("dragos", "1234"));
        manager.Utilizatori.Add(new Client("client", "client"));

        manager.AdaugaParcare(new Parcare("Central Parking", "A", 100));
        manager.AdaugaParcare(new Parcare("Mall Parking", "B", 500));
        manager.AdaugaParcare(new Parcare("Stadion Arena", "C", 2000));

        manager.AdaugaTip(new TipAbonament("Standard", 100, 30, "B"));
        manager.AdaugaTip(new TipAbonament("Premium", 250, 30, "A"));
        manager.AdaugaTip(new TipAbonament("Supporter", 50, 30, "C"));
    }
    
}