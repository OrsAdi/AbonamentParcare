using Proiect_POO;

namespace Interfata.Menus;

public class ClientMenu
{
    private readonly Client _client;
    private readonly SubscriptionManager _manager;

    public ClientMenu(Client client, SubscriptionManager manager)
    {
        _client = client;
        _manager = manager;
    }

    public void Show()
    {
        // =============================================================
        // AICI DAI PASTE LA TOT CODUL TĂU DE CLIENT
        // =============================================================

        // ATENȚIE:
        // 1. Unde aveai variabila 'c' sau 'client' -> folosește '_client'
        // 2. Unde aveai variabila 'manager' -> folosește '_manager'

        
        
        Console.WriteLine($"BINE AI VENIT CLIENT: {_client.Username}");

        // Codul tău original (exemplu):
        Console.WriteLine("CLIENT – abonamente disponibile:");
        _manager.Tipuri.ForEach(Console.WriteLine);
        
        // ... restul logicii tale de cumpărare ...
    
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"--- BINE AI VENIT, {_client.Username} ---");
            Console.WriteLine("1. Vezi abonamentele mele active");
            Console.WriteLine("2. CUMPARA un abonament (Alege parcare)");
            Console.WriteLine("3. Logout");
            Console.Write("Alege: ");
            var optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1":
                    AfiseazaAbonamenteClient(_client);
                    break;
                case "2":
                    CumparaAbonamentMeniu(_client, _manager);
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
}