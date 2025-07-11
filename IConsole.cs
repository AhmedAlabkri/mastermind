namespace Mastermind;

/// <summary>Console abstraction for testability.</summary>
public interface IConsole
{
    string? ReadLine();
    void Write(string value);
    void WriteLine(string value = "");
}

/// <summary>System console implementation.</summary>
public sealed class SystemConsole : IConsole
{
    public string? ReadLine()          => Console.ReadLine();
    public void Write(string value)    => Console.Write(value);
    public void WriteLine(string value = "") => Console.WriteLine(value);
}
