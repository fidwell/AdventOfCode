namespace AdventOfCode.ConsoleApp;

internal static class ConsoleWriter
{
    private static ConsoleColor Default => ConsoleColor.Gray;

    internal static void Error(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Console.ForegroundColor = Default;
    }

    internal static void Info(string message)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine(message);
        Console.ForegroundColor = Default;
    }

    internal static void Write(string message, ConsoleColor consoleColor = ConsoleColor.Gray)
    {
        Console.ForegroundColor = consoleColor;
        Console.WriteLine(message);
        Console.ForegroundColor = Default;
    }
}
