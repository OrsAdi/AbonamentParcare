namespace Interfata.Input;

public class ConsoleInput // : IInput (dupa ce creezi interfata)
{
    public string ReadLine()
    {
        return Console.ReadLine() ?? string.Empty;
    }
}