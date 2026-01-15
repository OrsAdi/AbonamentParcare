using Proiect_POO;
using Interfata.Menus;
using Infrastructura.Repositories;
using Interfata.Input;
using Interfata.Output; 

class Program
{
    static void Main(string[] args)
    {

        var consoleOut = new ConsoleOutput();
        var consoleIn = new ConsoleInput();


        var userRepo = new UserRepositoryJson();
        var parcareRepo = new ParcareRepositoryJson();
        var tipuriRepo = new TipAbonamentRepositoryJson();
        
        var manager = new SubscriptionManager();
        
        var users = userRepo.Incarca();
        
        if (users.Count == 0)
        {
            users.Add(new Admin("admin", "admin"));
            userRepo.Salveaza(users);
        }

        foreach (var u in users) manager.Utilizatori.Add(u);
        
        var parcari = parcareRepo.Incarca();
        if (parcari.Count == 0)
        {
            parcari.Add(new Parcare("Parcare Centrala", "Centru", 50));
            parcareRepo.Salveaza(parcari);
        }
        manager.Parcari.AddRange(parcari);
        
        
        var tipuri = tipuriRepo.Incarca();
        if (tipuri.Count == 0)
        {
            tipuri.Add(new TipAbonament("Standard(default)", 100, 30 , "A"));
            tipuriRepo.Salveaza(tipuri);
        }
        manager.Tipuri.AddRange(tipuri);
       

        manager.AdaugaTip(new TipAbonament("Standard", 150, 30, "A"));
        manager.AdaugaTip(new TipAbonament("Premium", 300, 30, "A"));
        manager.AdaugaTip(new TipAbonament("Standard", 100, 30, "B"));
        manager.AdaugaTip(new TipAbonament("Premium", 300, 30, "B"));
        manager.AdaugaTip(new TipAbonament("Standard", 200, 30, "C"));
        manager.AdaugaTip(new TipAbonament("Premium", 300, 30, "C"));


        // 2. BUCLA INFINITĂ (Aici este secretul interactivității)
        while (true)
        {
            Console.Clear(); 
            consoleOut.WriteLine("=== SISTEM PARCARE ===");
            consoleOut.WriteLine("1. Autentificare");
            consoleOut.WriteLine("2. Inregistrare");
            consoleOut.WriteLine("3. Iesire");
            consoleOut.Write("Alege optiunea: ");

            var comanda = consoleIn.ReadLine();

            switch (comanda)
            {
                case "1":
                    Console.Clear();
                    ProcesLogin(manager, userRepo, parcareRepo, tipuriRepo);
                    break;
                case "2":
                    Console.Clear();
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

    static void ProcesLogin(SubscriptionManager manager, UserRepositoryJson userRepo, ParcareRepositoryJson parcareRepo, TipAbonamentRepositoryJson tipuriRepo)
    {
        var user = LoginService.Login(manager.Utilizatori);
        if (user != null)
        {
            Console.WriteLine($"Bine ai venit, {user.Username}!");
            Thread.Sleep(1000);
            
            if(user is Admin adminUser)
            {
                new AdminMenu(adminUser,manager,userRepo, parcareRepo, tipuriRepo).Show();
            }
            else if (user is Client clientUser)
            {
                new ClientMenu(clientUser,manager, userRepo).Show();
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
    
}