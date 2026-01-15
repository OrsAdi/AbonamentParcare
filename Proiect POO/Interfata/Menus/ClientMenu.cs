using Proiect_POO;
using Infrastructura.Repositories;

namespace Interfata.Menus;

public class ClientMenu
{
    private readonly Client _client;
    private readonly SubscriptionManager _manager;
    private readonly UserRepositoryJson _userRepository;

    public ClientMenu(Client client, SubscriptionManager manager, UserRepositoryJson userRepo)
    {
        _client = client;
        _manager = manager;
        _userRepository = userRepo;
    }

    public void Show()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine($"--- CLIENT PANEL ({_client.Username}) ---");
            Console.WriteLine("1. Cumpara abonament nou");
            Console.WriteLine("2. Abonamente active (Curente)");
            Console.WriteLine("3. Istoric Abonamente (Expirate/Anulate)");
            Console.WriteLine("4. Anuleaza un abonament activ");
            Console.WriteLine("0. Deconectare");
            Console.Write("Optiune: ");

            string optiune = Console.ReadLine();

            switch (optiune)
            {
                case "1":
                    CumparaAbonament();
                    break;
                case "2":
                    AfiseazaAbonamenteActive();
                    break;
                case "3":
                    AfiseazaIstoric();
                    break;
                case "4":
                    AnuleazaAbonament();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Optiune invalida.");
                    Wait();
                    break;
            }
        }
    }

    private void CumparaAbonament()
    {
        Console.WriteLine("\n--- OFERTE DISPONIBILE ---");
        
        if (_manager.Tipuri.Count == 0)
        {
            Console.WriteLine("Nu exista oferte configurate momentan.");
            Wait();
            return;
        }

        for (int i = 0; i < _manager.Tipuri.Count; i++)
        {
            var tip = _manager.Tipuri[i];
            Console.WriteLine($"{i + 1}. [Zona {tip.ZonaPermisa}] {tip.Nume} - Pret: {tip.Pret} RON ({tip.ValabilitateZile} zile)");
        }

        Console.WriteLine("\n------------------------------------------------");
        Console.Write("Alege numarul ofertei (sau 0 pentru anulare): ");
        
        string input = Console.ReadLine();
        
        if (int.TryParse(input, out int index) && index > 0 && index <= _manager.Tipuri.Count)
        {
            var tipAles = _manager.Tipuri[index - 1];

            bool succes = _manager.CumparaAbonament(_client, tipAles);

            if (succes)
            {
                Console.WriteLine("SUCCES! Abonamentul a fost activat.");
                try 
                {
                    _userRepository.Salveaza(_manager.Utilizatori);
                    Console.WriteLine("Datele au fost salvate.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Eroare la salvare: {ex.Message}");
                }
            }
        }
        
        Wait();
    }

    private void AfiseazaAbonamenteActive()
    {
        Console.WriteLine("\n--- ABONAMENTE ACTIVE ---");
        
        var active = _client.Abonamente
            .Where(a => a.Activ && DateTime.Now <= a.DataStart.AddDays(a.Tip.ValabilitateZile))
            .ToList();

        if (active.Count == 0)
        {
            Console.WriteLine("Nu ai niciun abonament valid in acest moment.");
        }
        else
        {
            foreach (var ab in active)
            {
                var dataExp = ab.DataStart.AddDays(ab.Tip.ValabilitateZile);
                var zileRamase = (dataExp - DateTime.Now).Days;
                
                Console.WriteLine($"-> [Zona {ab.Tip.ZonaPermisa}] {ab.Tip.Nume}");
                Console.WriteLine($"   Expira la: {dataExp:d} (Mai ai {zileRamase} zile)");
                Console.WriteLine("-----------------------------------");
            }
        }
        Wait();
    }

    private void AfiseazaIstoric()
    {
        Console.WriteLine("\n--- ISTORIC ABONAMENTE ---");

        var istoric = _client.Abonamente
            .Where(a => !a.Activ || DateTime.Now > a.DataStart.AddDays(a.Tip.ValabilitateZile))
            .ToList();

        if (istoric.Count == 0)
        {
            Console.WriteLine("Nu ai istoric (toate abonamentele sunt active sau nu ai cumparat nimic).");
        }
        else
        {
            foreach (var ab in istoric)
            {
                var dataExp = ab.DataStart.AddDays(ab.Tip.ValabilitateZile);
                string status = !ab.Activ ? "ANULAT" : "EXPIRAT";

                Console.WriteLine($"-> [Status: {status}] {ab.Tip.Nume} (Zona {ab.Tip.ZonaPermisa})");
                Console.WriteLine($"   A fost valabil intre: {ab.DataStart:d} si {dataExp:d}");
                Console.WriteLine("-----------------------------------");
            }
        }
        Wait();
    }

    private void AnuleazaAbonament()
    {
        Console.WriteLine("\n--- ANULARE ABONAMENT ---");

        var active = _client.Abonamente
            .Where(a => a.Activ && DateTime.Now <= a.DataStart.AddDays(a.Tip.ValabilitateZile))
            .ToList();

        if (active.Count == 0)
        {
            Console.WriteLine("Nu ai niciun abonament activ pe care sa il poti anula.");
            Wait();
            return;
        }

        for (int i = 0; i < active.Count; i++)
        {
            var ab = active[i];
            Console.WriteLine($"{i + 1}. {ab.Tip.Nume} (Zona {ab.Tip.ZonaPermisa}) - Expira: {ab.DataStart.AddDays(ab.Tip.ValabilitateZile):d}");
        }

        Console.Write("\nAlege numarul abonamentului de anulat (0 pentru iesire): ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= active.Count)
        {
            var deAnulat = active[index - 1];
            
            Console.WriteLine($"Esti sigur ca vrei sa anulezi '{deAnulat.Tip.Nume}'? (Da/Nu)");
            string confirmare = Console.ReadLine()?.ToUpper();

            if (confirmare == "DA")
            {
                deAnulat.Anuleaza(); 
                
                try 
                {
                    _userRepository.Salveaza(_manager.Utilizatori);
                    Console.WriteLine("Abonamentul a fost ANULAT si mutat la Istoric.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Eroare la salvare: {ex.Message}");
                }
            }
            else
            {
                Console.WriteLine("Anulare operatiune.");
            }
        }
        Wait();
    }

    private void Wait()
    {
        Console.WriteLine("\nApasa Enter pentru a continua...");
        Console.ReadLine();
    }
}