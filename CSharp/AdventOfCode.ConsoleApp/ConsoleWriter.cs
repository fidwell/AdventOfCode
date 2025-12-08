namespace AdventOfCode.ConsoleApp;

internal static class ConsoleWriter
{
    private static ConsoleColor Default => ConsoleColor.Gray;

    internal static void Error(string message) => Write(message, ConsoleColor.Red);

    internal static void Info(string message) => Write(message, ConsoleColor.Cyan);

    internal static void Answer(int part, object answer, TimeSpan duration)
    {
        Console.Write($"Part {part} answer: ");
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(answer);
        Console.ForegroundColor = Default;
        Console.Write(" (");
        WriteTime(duration, pad: false);
        Console.WriteLine(")");
    }

    internal static void Write(string message, ConsoleColor consoleColor = ConsoleColor.Gray)
    {
        Console.ForegroundColor = consoleColor;
        Console.WriteLine(message);
        Console.ForegroundColor = Default;
    }

    internal static void WriteTime(TimeSpan duration, bool useColor = true, bool pad = true)
    {
        if (useColor)
        {
            Console.ForegroundColor = TimeColor(duration);
        }

        Console.Write(ElapsedTimeString(duration, pad).PadLeft(pad ? 11 : 0, ' '));
        Console.ForegroundColor = Default;
    }

    private static string ElapsedTimeString(TimeSpan duration, bool pad) =>
        duration.TotalMilliseconds < 1
            ? $"{duration.TotalMicroseconds:N1} μs"
            : duration.TotalSeconds < 1
                ? $"{duration.TotalMilliseconds:N1} ms"
                : $"{duration.TotalSeconds:N1} s{(pad ? " " : "")}";

    private static ConsoleColor TimeColor(TimeSpan duration) => duration switch
    {
        _ when duration.TotalSeconds > 10 => ConsoleColor.DarkRed,
        _ when duration.TotalSeconds > 5 => ConsoleColor.Red,
        _ when duration.TotalSeconds > 1 => ConsoleColor.DarkYellow,
        _ when duration.TotalMilliseconds > 500 => ConsoleColor.Yellow,
        _ when duration.TotalMilliseconds > 250 => ConsoleColor.Green,
        _ when duration.TotalMilliseconds > 1 => ConsoleColor.DarkGreen,
        _ when duration.TotalMicroseconds > 500 => ConsoleColor.Blue,
        _ => ConsoleColor.DarkBlue
    };
}
