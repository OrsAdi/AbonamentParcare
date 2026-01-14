namespace Interfata.Output;

public class ConsoleOutput // : IOutput
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }
    
    public void Write(string message)
    {
        Console.Write(message);
    }
}