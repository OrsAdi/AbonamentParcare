namespace Proiect_POO;

class Program
{
    static void Main()
    {
        var manager = new SubscriptionManager();
        InitializareDateDemo(manager);

        while (true)
        {
            Console.Clear();
            Console.WriteLine("=== SISTEM PARCARE ===");
            Console.WriteLine("1. Login");
            Console.WriteLine("2. Iesire");
            Console.Write("Alege o optiune: ");
            var optiune = Console.ReadLine();

            if (optiune == "2") break;

            if (optiune == "1")
            {
                var user = LoginService.Login(manager.Utilizatori);

                if (user != null)
                {
                    if (user is Admin admin)
                    {
                        MeniuAdmin(admin, manager);
                    }
                    else if (user is Client client)
                    {
                        MeniuClient(client, manager);
                    }
                }
                else
                {
                    Console.WriteLine("Apasa orice tasta pentru a continua...");
                    Console.ReadKey();
                }
            }
        }
    }

    static void MeniuAdmin(Admin admin, SubscriptionManager manager)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"--- PANOU ADMIN ({admin.Username}) ---");
            Console.WriteLine("1. Vezi toate parcarile");
            Console.WriteLine("2. Vezi tipurile de abonament existente");
            Console.WriteLine("3. ADAUGA un tip nou de abonament");
            Console.WriteLine("4. Logout");
            Console.Write("Alege: ");
            var optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1":
                    Console.WriteLine("\nPARCARI:");
                    manager.Parcari.ForEach(Console.WriteLine);
                    break;
                case "2":
                    Console.WriteLine("\nTIPURI ABONAMENT:");
                    manager.Tipuri.ForEach(Console.WriteLine);
                    break;
                case "3":
                    AdaugaTipAbonament(manager);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Optiune invalida!");
                    break;
            }
            Console.WriteLine("\nApasa orice tasta pentru a continua...");
            Console.ReadKey();
        }
    }

    static void AdaugaTipAbonament(SubscriptionManager manager)
    {
        Console.WriteLine("\n--- ADAUGARE TIP ABONAMENT ---");
        
        Console.Write("Nume abonament (ex: Gold, VIP): ");
        string nume = Console.ReadLine();

        Console.Write("Pret (RON): ");
        if (decimal.TryParse(Console.ReadLine(), out decimal pret))
        {
            int zile = 30; 
            
            Console.Write("Zona (A/B/C): ");
            string zona = Console.ReadLine();

            var tipNou = new TipAbonament(nume, pret, zile, zona);
            manager.AdaugaTip(tipNou);
            Console.WriteLine("Succes! Tipul de abonament a fost adaugat.");
        }
        else
        {
            Console.WriteLine("Pret invalid!");
        }
    }

    static void MeniuClient(Client client, SubscriptionManager manager)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"--- BINE AI VENIT, {client.Username} ---");
            Console.WriteLine("1. Vezi abonamentele mele active");
            Console.WriteLine("2. CUMPARA un abonament nou");
            Console.WriteLine("3. Logout");
            Console.Write("Alege: ");
            var optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1":
                    AfiseazaAbonamenteClient(client);
                    break;
                case "2":
                    CumparaAbonamentMeniu(client, manager);
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Optiune invalida!");
                    break;
            }
            Console.WriteLine("\nApasa orice tasta pentru a continua...");
            Console.ReadKey();
        }
    }

    static void AfiseazaAbonamenteClient(Client client)
    {
        Console.WriteLine("\nABONAMENTELE TALE:");
        if (client.Abonamente.Count == 0)
        {
            Console.WriteLine("Nu ai niciun abonament activ.");
        }
        else
        {
            foreach (var ab in client.Abonamente)
            {
                var dataExpirare = ab.DataStart.AddDays(ab.Tip.ValabilitateZile);
                Console.WriteLine($"- {ab.Tip.Nume} | Zona {ab.Tip.ZonaPermisa} | Expira la: {dataExpirare:dd/MM/yyyy}");
            }
        }
    }

    static void CumparaAbonamentMeniu(Client client, SubscriptionManager manager)
    {
        Console.WriteLine("\n--- OFERTA DISPONIBILA ---");
        for (int i = 0; i < manager.Tipuri.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {manager.Tipuri[i]}");
        }

        Console.Write("\nAlege numarul abonamentului dorit (sau 0 pentru anulare): ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= manager.Tipuri.Count)
        {
            var tipAles = manager.Tipuri[index - 1];
            manager.CumparaAbonament(client, tipAles);
            
            Console.WriteLine($"\nFelicitari! Ai cumparat abonamentul {tipAles.Nume}.");
            Console.WriteLine($"Este valabil 30 de zile, pana la: {DateTime.Now.AddDays(30):dd/MM/yyyy}");
        }
        else if (index != 0)
        {
            Console.WriteLine("Optiune invalida.");
        }
    }

    static void InitializareDateDemo(SubscriptionManager manager)
    {
        manager.Utilizatori.Add(new Admin("admin", "admin"));
        manager.Utilizatori.Add(new Client("dragos", "1234"));
        manager.Utilizatori.Add(new Client("client", "client"));

        manager.AdaugaParcare(new Parcare("Central Parking", "A", 100));
        manager.AdaugaParcare(new Parcare("Mall Parking", "B", 500));

        manager.AdaugaTip(new TipAbonament("Standard", 100, 30, "B"));
        manager.AdaugaTip(new TipAbonament("VIP", 250, 30, "A"));
    }
}