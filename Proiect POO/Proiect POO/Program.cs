using System.Linq;

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
            Console.WriteLine("4. ADAUGA o parcare noua");
            Console.WriteLine("5. Logout");
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
                    AdaugaParcare(manager);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Optiune invalida!");
                    break;
            }
            Console.WriteLine("\nApasa orice tasta pentru a continua...");
            Console.ReadKey();
        }
    }

    static void AdaugaParcare(SubscriptionManager manager)
    {
        Console.WriteLine("\n--- ADAUGARE PARCARE NOUA ---");

        Console.Write("Nume parcare: ");
        string nume = Console.ReadLine();

        Console.Write("Zona (ex: A, B, Centru): ");
        string zona = Console.ReadLine();

        Console.Write("Capacitate (nr locuri): ");
        if (int.TryParse(Console.ReadLine(), out int capacitate))
        {
            var parcareNoua = new Parcare(nume, zona, capacitate);
            manager.AdaugaParcare(parcareNoua);
            Console.WriteLine("Succes! Parcarea a fost adaugata.");
        }
        else
        {
            Console.WriteLine("Capacitate invalida!");
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
            Console.WriteLine("2. CUMPARA un abonament (Alege parcare)");
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
                string status = ab.EsteExpirat() ? "[EXPIRAT]" : "[ACTIV]";
                Console.WriteLine($"{status} {ab.Tip.Nume} | Zona {ab.Tip.ZonaPermisa} | Expira la: {dataExpirare:dd/MM/yyyy}");
            }
        }
    }

    static void CumparaAbonamentMeniu(Client client, SubscriptionManager manager)
    {
        Console.WriteLine("\n--- ALEGE PARCAREA DORITA ---");
        
        if (manager.Parcari.Count == 0)
        {
            Console.WriteLine("Nu exista parcari in sistem.");
            return;
        }

        for (int i = 0; i < manager.Parcari.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {manager.Parcari[i].Nume} (Zona {manager.Parcari[i].Zona})");
        }

        Console.Write("\nIntrodu numarul parcarii: ");
        if (int.TryParse(Console.ReadLine(), out int indexParcare) && indexParcare > 0 && indexParcare <= manager.Parcari.Count)
        {
            var parcareAleasa = manager.Parcari[indexParcare - 1];
            Console.WriteLine($"\nAi ales parcarea: {parcareAleasa.Nume}. Cautam oferte disponibile...");

            var toateAbonamenteleZona = manager.Tipuri
                .Where(t => t.ZonaPermisa == parcareAleasa.Zona)
                .ToList();

            var oferteValide = toateAbonamenteleZona
                .Where(tip => !client.Abonamente.Any(a => a.Tip.Nume == tip.Nume && a.Activ && !a.EsteExpirat()))
                .ToList();

            if (oferteValide.Count == 0)
            {
                Console.WriteLine($"\nAi deja toate abonamentele disponibile pentru Zona {parcareAleasa.Zona} (sau nu exista oferte).");
                return;
            }

            Console.WriteLine($"\n--- OFERTE DISPONIBILE PENTRU TINE (ZONA {parcareAleasa.Zona}) ---");
            for (int i = 0; i < oferteValide.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {oferteValide[i]}");
            }

            Console.Write("\nAlege abonamentul dorit: ");
            if (int.TryParse(Console.ReadLine(), out int indexAb) && indexAb > 0 && indexAb <= oferteValide.Count)
            {
                var tipAles = oferteValide[indexAb - 1];
                manager.CumparaAbonament(client, tipAles);

                Console.WriteLine($"\nSucces! Ai cumparat abonamentul {tipAles.Nume}.");
                Console.WriteLine($"Valabil pana la: {DateTime.Now.AddDays(30):dd/MM/yyyy}");
            }
            else
            {
                Console.WriteLine("Optiune invalida.");
            }
        }
        else
        {
            Console.WriteLine("Parcare invalida.");
        }
    }

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