using Proiect_POO;
using Infrastructura.Repositories;

namespace Interfata.Menus;

public class ClientMenu
{
    private readonly Client _client;
    private readonly SubscriptionManager _manager;
    private readonly UserRepositoryJson _userRepository;
    public ClientMenu(Client client, SubscriptionManager manager,  UserRepositoryJson userRepository)
    {
        _client = client;
        _manager = manager;
        _userRepository = userRepository;
    }

    public void Show()
    {
        

        
        
        Console.WriteLine($"BINE AI VENIT CLIENT: {_client.Username}");

        Console.WriteLine("CLIENT – abonamente disponibile:");
        _manager.Tipuri.ForEach(Console.WriteLine);
        
        // logica de cumparare
    
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
                    CumparaAbonament();
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

            Console.WriteLine($"\nAi selectat: {tipAles.Nume} pentru Zona {tipAles.ZonaPermisa}");
            Console.WriteLine("Se proceseaza...");
            Thread.Sleep(800); 

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
                    Console.WriteLine($"Atentie: Eroare la salvarea pe disc: {ex.Message}");
                }
            }
        }
        else if (input != "0")
        {
            Console.WriteLine("Numar invalid.");
        }
        
        Wait();
    }
    
    private void Wait()
    {
        Console.WriteLine("\nApasa Enter pentru a continua...");
        Console.ReadLine();
    }
}