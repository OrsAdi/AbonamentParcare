using Proiect_POO;
using Infrastructura.Repositories;


namespace Interfata.Menus;

public class AdminMenu
{
    private readonly Admin _admin;
    private readonly SubscriptionManager _manager;
    private readonly UserRepositoryJson _userRepositoryJson;
    private readonly ParcareRepositoryJson _parcareRepo;
    private readonly TipAbonamentRepositoryJson _tipuriRepo;

    public AdminMenu(Admin admin, SubscriptionManager manager, UserRepositoryJson userRepositoryJson, ParcareRepositoryJson parcareRepo, TipAbonamentRepositoryJson tipuriRepo)
    {
        _admin = admin;
        _manager = manager;
        _userRepositoryJson = userRepositoryJson;
        _parcareRepo = parcareRepo;
        _tipuriRepo = tipuriRepo;
    }

    public void Show()
    {
        Console.WriteLine($"BINE AI VENIT ADMIN: {_admin.Username}");
        
        while(true)
        {
            Console.Clear();
            Console.WriteLine($"--- PANOU ADMIN ({_admin.Username}) ---");
            Console.WriteLine("1. GESTIONARE PARCARI (Adauga/Sterge)");
            Console.WriteLine("2. GESTIONARE ABONAMENTE (Adauga/Sterge)");
            Console.WriteLine("0. Iesire");
            Console.Write("Optiune: ");
            var optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1":
                    Console.WriteLine("\nPARCARI:");
                    MeniuParcari();
                    break;
                case "2":
                    Console.WriteLine("\nMeniu Tipuri:");
                    MeniuTipuri();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Optiune invalida!");
                    break;
            }
            Console.WriteLine("\nApasa orice tasta pentru a continua...");
            Console.ReadKey();
        }
    }
    
    
    private void MeniuParcari()
    {
        Console.WriteLine("\n--- PARCARI ---");
        _manager.Parcari.ForEach(p => Console.WriteLine($"- {p}"));
        
        Console.WriteLine("\n[A]dauga parcare | [S]terge parcare | [I]napoi");
        var key = Console.ReadLine()?.ToUpper();

        if (key == "A")
        {
            Console.Write("Nume: "); string nume = Console.ReadLine();
            Console.Write("Adresa: "); string adresa = Console.ReadLine();
            Console.Write("Locuri: "); int.TryParse(Console.ReadLine(), out int locuri);

            var p = new Parcare(nume, adresa, locuri);
            _manager.Parcari.Add(p); // 1. Memorie
            _parcareRepo.Salveaza(_manager.Parcari); // 2. Fisier
            Console.WriteLine("Parcare salvata!");
        }
        else if (key == "S")
        {
            Console.Write("Numele parcarii de sters: ");
            string nume = Console.ReadLine();
            var deSters = _manager.Parcari.FirstOrDefault(p => p.Nume == nume);
            
            if (deSters != null) {
                _manager.Parcari.Remove(deSters);
                _parcareRepo.Salveaza(_manager.Parcari);
                Console.WriteLine("Parcare stersa!");
            }
        }
        Wait();
    }

    private void MeniuTipuri()
    {
        Console.WriteLine("\n--- TIPURI ABONAMENTE ---");
        _manager.Tipuri.ForEach(t => Console.WriteLine($"- {t}"));

        Console.WriteLine("\n[A]dauga tip | [S]terge tip | [I]napoi");
        var key = Console.ReadLine()?.ToUpper();

        if (key == "A")
        {
            Console.Write("Nume: "); string nume = Console.ReadLine();
            Console.Write("Pret: "); decimal.TryParse(Console.ReadLine(), out decimal pret);
            Console.Write("Zile: "); int.TryParse(Console.ReadLine(), out int zile);
            Console.Write("Zona: "); string zona = Console.ReadLine();

            var t = new TipAbonament(nume, pret, zile, zona);
            _manager.Tipuri.Add(t);
            _tipuriRepo.Salveaza(_manager.Tipuri); 
            Console.WriteLine("Tip abonament salvat!");
        }
        else if (key == "S")
        {
            Console.Write("Numele tipului de sters: ");
            string nume = Console.ReadLine();
            var deSters = _manager.Tipuri.FirstOrDefault(t => t.Nume == nume);

            if (deSters != null)
            {
                _manager.Tipuri.Remove(deSters);
                _tipuriRepo.Salveaza(_manager.Tipuri);
                Console.WriteLine("Tip sters!");
            }
        }
        Wait();
    }
    
    private void Wait() { Console.WriteLine("Enter..."); Console.ReadLine(); }

    

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