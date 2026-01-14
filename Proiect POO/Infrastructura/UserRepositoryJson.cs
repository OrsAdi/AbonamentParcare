using Proiect_POO;

namespace Infrastructura;

// Extindem clasa generica specificand ca lucram cu 'User'
public class UserRepositoryJson : JsonFileStocare<User>
{
    public UserRepositoryJson() : base("users.json")
    {
    }

    // Aici poti adauga metode specifice daca ai nevoie, ex:
    // public User? FindByUsername(string username) { ... }
}