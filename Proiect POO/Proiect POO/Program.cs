namespace Proiect_POO;

class Program
{

    static void Main()
    {
        var manager = new SubscriptionManager();

        var admin = new Admin("admin", "admin");
        var client = new Client("dragos", "1234");

        manager.Utilizatori.Add(admin);
        manager.Utilizatori.Add(client);

        manager.AdaugaParcare(new Parcare("Central Parking", "A", 100));
        manager.AdaugaTip(new TipAbonament("Standard", 150, 30, "A"));
        manager.AdaugaTip(new TipAbonament("Premium", 300, 30, "A"));

        var user = LoginService.Login(manager.Utilizatori);

        if (user is Admin)
        {
            Console.WriteLine("ADMIN – parcări și abonamente:");
            manager.Parcari.ForEach(Console.WriteLine);
            manager.Tipuri.ForEach(Console.WriteLine);
        }
        else if (user is Client c)
        {
            Console.WriteLine("CLIENT – abonamente disponibile:");
            manager.Tipuri.ForEach(Console.WriteLine);

            manager.CumparaAbonament(c, manager.Tipuri[0]);

            Console.WriteLine("Abonamentele tale:");
            c.Abonamente.ForEach(Console.WriteLine);
        }
    }
}