using Proiect_POO;
using Infrastructura.Repositories;


namespace Interfata.Menus;

public class AdminMenu
{
    private readonly Admin _admin;
    private readonly SubscriptionManager _manager;
    private readonly UserRepositoryJson _userRepositoryJson;

    public AdminMenu(Admin admin, SubscriptionManager manager, UserRepositoryJson userRepositoryJson)
    {
        _admin = admin;
        _manager = manager;
        _userRepositoryJson = userRepositoryJson;
    }

    public void Show()
    {
        
        
        
        // =============================================================
        // AICI DAI PASTE LA TOT CODUL TĂU DE ADMIN (cele 100 de linii)
        // =============================================================
        
        // ATENȚIE:
        // 1. Unde aveai variabila 'user' sau 'admin' -> folosește '_admin'
        // 2. Unde aveai variabila 'manager' -> folosește '_manager'
        
        // Exemplu (codul tău probabil arată cam așa):
        Console.WriteLine($"BINE AI VENIT ADMIN: {_admin.Username}");
        
        while(true)
        {
            Console.Clear();
            Console.WriteLine($"--- PANOU ADMIN ({_admin.Username}) ---");
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
                    _manager.Parcari.ForEach(Console.WriteLine);
                    break;
                case "2":
                    Console.WriteLine("\nTIPURI ABONAMENT:");
                    _manager.Tipuri.ForEach(Console.WriteLine);
                    break;
                case "3":
                    AdaugaTipAbonament(_manager);
                    break;
                case "4":
                    AdaugaParcare(_manager);
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

}